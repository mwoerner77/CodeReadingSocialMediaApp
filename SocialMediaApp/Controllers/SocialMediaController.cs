namespace SocialMediaApp.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SocialMediaApp;
    using SocialMediaApp.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private AccountManager accountManager;
        private PostLibrary postLibrary;

        //1:
        //
        public SocialMediaController() 
        {
            this.accountManager = AccountManager.GetAccountManagerSingleInstance();
            this.postLibrary = PostLibrary.GetPostLibrarySingleInstance();
        }

        [HttpPost("CreateNewAccount")]
        public ActionResult<string> CreateNewAccount(AccountCreationItem accountCreationItem)
        {
            //2:
            //
            if (accountCreationItem.Username != null &&  accountCreationItem.Password != null && accountCreationItem.PasswordConfirmation != null) 
            {
                //3:
                //
                AccountCreationStatus statusCode = this.accountManager.CreateAccount(accountCreationItem.Username, accountCreationItem.Password, accountCreationItem.PasswordConfirmation);

                //4:
                //
                switch (statusCode)
                {
                    case AccountCreationStatus.PasswordMismatch:
                        return BadRequest("Passwords do not match");
                    case AccountCreationStatus.AlreadyExists:
                        return BadRequest($"The username {accountCreationItem.Username} is already taken");
                    default:
                        return Ok("Account Created");
                }
            }
            else
            {
                return BadRequest("Invalid Request Object");
            }
            
        }

        [HttpPost("CreatePost")]
        public ActionResult<string> CreatePost(PostCreationItem postCreationItem)
        {
            //5:
            //
            if (postCreationItem.Username != null && postCreationItem.Password != null && postCreationItem.Post != null)
            {
                //6:
                //
                AccountLoginStatus loginStatusCode = this.accountManager.LoginAccount(postCreationItem.Username, postCreationItem.Password);

                //7:
                //
                switch (loginStatusCode)
                {
                    case AccountLoginStatus.IncorrectPassword:
                        return Unauthorized("Password was incorrect");
                    case AccountLoginStatus.DoesNotExist:
                        return NotFound($"The user {postCreationItem.Username} does not exist");
                    default:
                        this.postLibrary.CreatePost(postCreationItem.Username, postCreationItem.Post);
                        return Ok("Post Created");
                }
            }
            else
            {
                return BadRequest("Invalid Request Object");
            }
        }

        [HttpPost("LikePost")]
        public ActionResult<string> LikePost(int postId)
        {
            //8:
            //
            PostActionItem postActionItem = new PostActionItem()
            {
                ActionName = "Like",
                PostId = postId
            };

            PostActionStatus postActionStatusCode = this.postLibrary.PerformPostAction(postActionItem);

            //9:
            //
            switch (postActionStatusCode)
            {
                case PostActionStatus.PostDoesNotExist:
                    return NotFound($"No post with ID: {postId} exists");
                default:
                    return Ok($"Post with ID: {postId} liked");
            }
        }

        [HttpPost("ViewPost")]
        public ActionResult<string> ViewPost(int postId)
        {
            //10:
            //
            PostActionItem postActionItem = new PostActionItem()
            {
                ActionName = "View",
                PostId = postId
            };

            PostActionStatus postActionStatusCode = this.postLibrary.PerformPostAction(postActionItem);

            //11:
            //
            switch (postActionStatusCode)
            {
                case PostActionStatus.PostDoesNotExist:
                    return NotFound($"No post with ID: {postId} exists");
                default:
                    return Ok($"Post with ID: {postId} viewed");
            }
        }

        [HttpGet("Posts/{username}")]
        public ActionResult<PostResponseItem> GetPosts(string username)
        {
            //11:
            //
            List<PostItem?>? posts = this.postLibrary.GetPostsFromUser(username);

            //12:
            //
            PostResponseItem postResponseItem = new PostResponseItem()
            {
                Posts = posts
            };

            return Ok(postResponseItem);
        }
    }
}
