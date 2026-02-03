using LibraryManager.Application.DTOs.Request;
using LibraryManager.Application.DTOs.Response;
using LibraryManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        /// <summary>
        ///  a sorted list by Author (last, first) then title
        ///
        /// </summary>
        /// <param name="request">PageRequestDTO</param>
        /// <returns>Apiresponse</returns>
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
        /// <summary>
        /// sorted list of these by Publisher, Author (last, first), then title.
        /// </summary>
        /// <param name="request">PageRequestDTO</param>
        /// <returns>Apiresponse</returns>
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
        /// <summary>
        /// the total price of all books in the database
        /// </summary>
        /// <returns>ApiResponse</returns>
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
        /// <summary>
        /// save the entire list to the database, with only one call to the DB server
        /// </summary>
        /// <param name="requestDTO">BookBulkSaveRequestDTO</param>
        /// <returns></returns>
        [HttpPost]
        [Route("bulk-save")]
        public async Task<IActionResult> BookBulkSave([FromBody] BookBulkSaveRequestDTO requestDTO)
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
