namespace SocialMediaApp
{
    using SocialMediaApp.Models;
    using SocialMediaApp.PostActions;
    using System.Collections.Generic;
    using System.Text.Json;

    public class PostLibrary
    {
        private PostActionFactory postActionFactory;
        private static PostLibrary postLibraryInstance = new PostLibrary();

        //57:
        //
        private PostLibrary()
        {
            this.postActionFactory = PostActionFactory.GetPostActionFactorySingleInstance();
        }

        //58:
        //
        public static PostLibrary GetPostLibrarySingleInstance()
        {
            return postLibraryInstance;
        }

        public void CreatePost(string username, string password, string postText)
        {
            //59:
            //
            string postsJsonText = File.ReadAllText("Posts.json");
            List<PostItem?>? posts = JsonSerializer.Deserialize<List<PostItem?>>(postsJsonText);

            //60:
            //
            posts?.Add(new PostItem()
            {
                Id = posts.Count,
                Username = username,
                Text = postText,
                Likes = 0,
                Views = 0,
                CreationTime = DateTime.Now
            });

            //61:
            //
            postsJsonText = JsonSerializer.Serialize(posts);
            File.WriteAllText("Posts.json", postsJsonText);
        }

        public void CreatePost(string username, string postText)
        {
            //62:
            //
            string postsJsonText = File.ReadAllText("Posts.json");
            List<PostItem?>? posts = JsonSerializer.Deserialize<List<PostItem?>>(postsJsonText);

            //63:
            //
            posts?.Add(new PostItem()
            {
                Id = posts.Count,
                Username = username,
                Text = postText,
                Likes = 0,
                Views = 0,
                CreationTime = DateTime.Now
            });

            //64:
            //
            postsJsonText = JsonSerializer.Serialize(posts);
            File.WriteAllText("Posts.json", postsJsonText);
        }

        public PostItem? GetPostFromUser(string username)
        {
            //65:
            //
            string postsJsonText = File.ReadAllText("Posts.json");
            List<PostItem?>? posts = JsonSerializer.Deserialize<List<PostItem?>>(postsJsonText);

            //66:
            //
            return posts?.Where(p => p?.Username == username).FirstOrDefault();
        }

        public List<PostItem?>? GetPostsFromUser(string username)
        {
            //67:
            //
            string postsJsonText = File.ReadAllText("Posts.json");
            List<PostItem?>? posts = JsonSerializer.Deserialize<List<PostItem?>>(postsJsonText);

            //68:
            //
            return posts?.Where(p => p?.Username == username).ToList();
        }

        public PostActionStatus PerformPostAction(PostActionItem postActionItem)
        {
            //69:
            //
            string postsJsonText = File.ReadAllText("Posts.json");
            List<PostItem?>? posts = JsonSerializer.Deserialize<List<PostItem?>>(postsJsonText);

            //70:
            //
            PostActionStatus status = PostActionStatus.PostDoesNotExist;
            IPostAction? postAction = this.postActionFactory.GetPostAction(postActionItem.ActionName);

            //71:
            //
            if (postAction != null)
            {
                //72:
                //
                status = postAction.PerformPostAction(postActionItem, posts);
                if (status == PostActionStatus.OK)
                {
                    postsJsonText = JsonSerializer.Serialize(posts);
                    File.WriteAllText("Posts.json", postsJsonText);
                }

            }

            return status;
        }

    }
}
