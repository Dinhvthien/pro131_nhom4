using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using App_Shared.Model;
using Pro131_Nhom4.Services;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IBillDetailsService _billDetailsService;
        private readonly IBillStatusServices _billStatusServices;
        private readonly IProductService _productService;
        private readonly ICartDetailsService _cartDetailsService;
        public BillController(IBillService billService, IBillDetailsService billDetailsService, IBillStatusServices billStatusServices, IProductService productService, ICartDetailsService cartDetailsService)
        {
            _billDetailsService = billDetailsService;
            _billService = billService;
            _billStatusServices = billStatusServices;
            _productService = productService;
            _cartDetailsService = cartDetailsService;
        }
        [HttpGet]
        public async Task<IActionResult> ShowListBill()
        {
            var bill = await _billService.GetAllBills();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(bill, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBillByAccountId([FromRoute] Guid id)
        {
            var bill = await _billService.GetBillByAccountId(id);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(bill, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }

        [HttpPost]

        public async Task<ActionResult<Bill>> CreateBill(List<CartDetails> cartDetails, string Adress)
        {
            Guid accountId = Guid.NewGuid();
            double price = 0;
            foreach (var item in cartDetails)
            {
                var product = _productService.GetProductById(item.ProductID);
                price += (product.Result.Price * item.Quantity);
                accountId = item.AccountID;
            }
            // Tạo một hóa đơn mới
            Bill bill = new Bill ()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StatusID = Guid.Parse("69bf7125-0780-4b06-b4b4-88f2cb4f9924"),
                VoucherID = Guid.Parse("730ade9a-25bd-4ad6-5c5f-08db911eda27"),
                PayMentID = Guid.Parse("b10bfbec-4e71-4732-9956-08db911dd6c0"),
                Address = Adress,

                Price = price,
                AccountID = accountId,
            };

            // Thêm hóa đơn vào danh sách
            await _billService.CreateBill(bill);

            foreach (var item in cartDetails)
            {
                var product = _productService.GetProductById(item.ProductID);
                product.Result.AvailableQuantity -= item.Quantity;
                await _productService.UpdateProduct(product.Result);

                // Tạo chi tiết hóa đơn mới
                BillDetails billDetail = new BillDetails()
                {
                    BillID = bill.Id,
                    ProductID = product.Result.Id,
                    Quantity = item.Quantity,
                    Prices = product.Result.Price * item.Quantity,
                };

                // Thêm chi tiết hóa đơn vào danh sách
                await _billDetailsService.CreateBillDetails(billDetail);
                await _cartDetailsService.DeleteCartDetails(item.Id);
            }

        /*    var account = .GetAccountById(accountId);
            if (price > 100000)
            {
                account.Result.Account.Point += (int)price / 10000;
                await _iAccountService.UpdateAccountPoint(account.Result);
            }*/

            return Ok(bill);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillController>> Delete(Guid id)
        {
            await _billService.DeleteBill(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BillView>> Update(BillView billView)
        {
            await _billService.UpdateBill(billView);
            return Ok(billView);
        }

    }
}
