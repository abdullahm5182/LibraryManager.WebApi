using LibraryManager.Application.DTOs.Request;
using LibraryManager.Application.DTOs.Response;
using LibraryManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        readonly IBookService bookService;
        readonly ILogger<BooksController> logger;
        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            this.bookService = bookService;
            this.logger =logger;
        }

        #region GetBooksSortedByAuthor 
        [HttpGet]
        [Route("sorted-by-author")]
        public async Task<IActionResult> GetBooksSortedByAuthor([FromQuery] PageRequestDTO request)
        {
            ApiResponse apiResponse = null;
            try
            {
                var response = await bookService.GetBooksSortedByAuthor(request);
                apiResponse = new ApiResponse
                {
                    Status = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Books Fetched Successfully Sorted By Author",
                    Response = response
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error While Fetching Book Sorted By Author {@request},Exception:{@ex}", request, ex);
                apiResponse = new ApiResponse
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "Error While Fetching Book Sorted By Author",
                    Response = null
                };
            }

            return new ObjectResult(apiResponse);

        }
        #endregion

        #region GetBooksSortedByPublisher 
        [HttpGet]
        [Route("sorted-by-publisher")]
        public async Task<IActionResult> GetBooksSortedByPublisher([FromQuery] PageRequestDTO request)
        {
            ApiResponse apiResponse = null;
            try
            {
                var response = await bookService.GetBooksSortedByPublisher(request);
                apiResponse = new ApiResponse
                {
                    Status = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Books Fetched Successfully Sorted By Publisher",
                    Response = response
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error While Fetching Book Sorted By Publisher {@request},Exception:{@ex}", request, ex);
                apiResponse = new ApiResponse
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "Error While Fetching Book Sorted By Publisher",
                    Response = null
                };
            }

            return new ObjectResult(apiResponse);

        }
        #endregion

        #region GetBooksTotalPrice 
        [HttpGet]
        [Route("total-price")]
        public async Task<IActionResult> GetBooksTotalPrice()
        {
            ApiResponse apiResponse = null;
            try
            {
                var response = await bookService.GetBooksTotalPrice();
                apiResponse = new ApiResponse
                {
                    Status = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Books Total Price Fetched Successfully",
                    Response = response
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error While Fetching Books Total Price ,Exception:{@ex}", ex);
                apiResponse = new ApiResponse
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "Error While Fetching Books Total Price",
                    Response = null
                };
            }

            return new ObjectResult(apiResponse);

        }
        #endregion

        #region BookBulkSave 
        [HttpPost]
        [Route("bulk-save")]
        public async Task<IActionResult> BookBulkSave([FromBody]BookBulkSaveRequestDTO requestDTO)
        {
            ApiResponse apiResponse = null;
            try
            {
                var response = await bookService.BooksBulkSave(requestDTO);
                apiResponse = new ApiResponse
                {
                    Status = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Books Saved Successfully",
                    Response = response
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error While Saving Books,Exception:{@ex}", ex);
                apiResponse = new ApiResponse
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "Error While Saving Books",
                    Response = null
                };
            }

            return new ObjectResult(apiResponse);

        }
        #endregion

    }
}
