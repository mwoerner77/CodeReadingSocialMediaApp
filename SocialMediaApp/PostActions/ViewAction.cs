namespace SocialMediaApp.PostActions
{
    using SocialMediaApp.Models;

    public class ViewAction : IPostAction
    {
        //26:
        //
        public string Name => "View";

        public PostActionStatus PerformPostAction(PostActionItem postActionItem, List<PostItem?>? posts)
        {
            //27:
            //
            if (posts != null)
            {
                for (int i = 0; i < posts.Count; i++)
                {
                    //28:
                    //
                    PostItem? post = posts[i];
                    if (post?.Id == postActionItem.PostId)
                    {
                        post.Views += 1;
                        posts[i] = post;
                        return PostActionStatus.OK;
                    }
                }
            }

            return PostActionStatus.PostDoesNotExist;
        }
    }
}
