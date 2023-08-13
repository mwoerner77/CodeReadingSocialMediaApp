namespace SocialMediaApp.PostActions
{
    using SocialMediaApp.Models;

    public class LikeAction : IPostAction
    {
        //23:
        //
        public string Name => "Like";

        public PostActionStatus PerformPostAction(PostActionItem postActionItem, List<PostItem?>? posts)
        {
            //24:
            //
            if (posts != null)
            {
                for (int i = 0; i < posts.Count; i++)
                {
                    //25:
                    //
                    PostItem? post = posts[i];
                    if (post?.Id == postActionItem.PostId)
                    {
                        post.Likes += 1;
                        posts[i] = post;
                        return PostActionStatus.OK;
                    }
                }
            }

            return PostActionStatus.PostDoesNotExist;
        }
    }
}
