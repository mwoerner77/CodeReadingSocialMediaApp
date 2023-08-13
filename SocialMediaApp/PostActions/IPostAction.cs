namespace SocialMediaApp.PostActions
{
    using SocialMediaApp.Models;

    //22:
    //
    public interface IPostAction
    {
        public string Name { get; }

        public PostActionStatus PerformPostAction(PostActionItem postActionItem, List<PostItem?>? posts);
    }
}
