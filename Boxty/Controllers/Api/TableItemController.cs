//using AutoMapper;
//using Boxty.Data;
//using Boxty.Models;
//using Boxty.Services;
//using Boxty.Services.Interfaces;
//using Boxty.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Boxty.Controllers.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TableItemController : Controller
//    {
//        private readonly BoxtyDbContext context;
//        private readonly IUserService userService;
//        private readonly IProductService productService;
//        private readonly IMapper mapper;
//        private readonly ITableItemService tableItemService;

//        public TableItemController(BoxtyDbContext context, IUserService userService, IProductService productService, IMapper mapper, ITableItemService tableItemService)
//        {
//            this.context = context;
//            this.userService = userService;
//            this.productService = productService;
//            this.mapper = mapper;
//            this.tableItemService = tableItemService;
//        }
        
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TableItemOutputModel>> GetTableItems(int tableId)
//        {
//            return await tableItemService.GetTableItems(tableId);
//        }

//        [HttpPost]
//        public async Task<JsonResult> SelectTableItem(TableItemInputModel model)
//        {
//            var a = new { Success = await tableItemService.SelectTableItem(model) };
//            return Json(a);
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteSelectedItem(int tableItemId)
//        {
//            return Json(await tableItemService.DeleteSelectedItem(tableItemId));
//        }

//        [HttpPost]
//        public async Task<ActionResult> OrderSelectedItems(int tableId)
//        {
//            return Json(await tableItemService.AddSelectedItems(tableItemId));
//        }
//    }
//}
