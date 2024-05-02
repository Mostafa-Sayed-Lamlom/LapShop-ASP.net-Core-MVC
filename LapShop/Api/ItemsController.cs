using LapShop.Api.Base;
using LapShop.EF.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace LapShop.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Api for getting all Items(products) form database
        /// </summary>
        /// <returns></returns>
        // GET: api/<ItemController>/ItemAction
        [HttpGet]
        public ApiBase GetAll()
        {
            ApiBase apiBase = new ApiBase();
            apiBase.Data = _itemService.GetAll();
            apiBase.StatusCode = "200";
            return apiBase;
        }

        /// <summary>
        /// Api for getting all Items(products) with same category
        /// </summary>
        /// <param name="id">the category Id</param>
        /// <returns></returns>
        // GET: api/<ItemController>/ItemAction
        [HttpGet("{id}")]
        public ApiBase GetAllByCategoryId(int id)
        {
            ApiBase apiBase = new ApiBase();
            apiBase.Data = _itemService.GetAllRelatedData(id);
            apiBase.StatusCode = "200";
            return apiBase;
        }

        /// <summary>
        /// Api for getting Item(product) by Item Id
        /// </summary>
        /// <param name="id">the Item Id</param>
        /// <returns></returns>
        // GET api/<ItemController>/ItemAction/5
        [HttpGet("{id}")]
        public ApiBase GetById(int id)
        {
            ApiBase apiBase = new ApiBase();
            apiBase.Data = _itemService.GetById(id)!;
            apiBase.StatusCode = "200";
            return apiBase;
        }

        //// POST api/<ItemController>/ItemAction
        //[HttpPost]
        //public void Save([FromBody] string value)
        //{
        //}

        //// PUT api/<ItemController>/ItemAction/5
        //[HttpPut("{id}")]
        //public void Update(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ItemController>/ItemAction/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
