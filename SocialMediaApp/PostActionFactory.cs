namespace SocialMediaApp
{
    using SocialMediaApp.PostActions;
  
    public class PostActionFactory
    {
        private LikeAction likeAction;
        private ViewAction viewAction;
        private static PostActionFactory postActionFactory = new PostActionFactory();

        //53:
        //
        private PostActionFactory()
        {
            this.likeAction = new LikeAction();
            this.viewAction = new ViewAction();
        }

        //54:
        //
        public static PostActionFactory GetPostActionFactorySingleInstance()
        {
            return postActionFactory;
        }

        public IPostAction? GetPostAction(string? actionName)
        {
            //55:
            //
            if (actionName !=  null)
            {
                //56:
                //
                switch (actionName.ToLower())
                {
                    case "like":
                        return this.likeAction;
                    case "view":
                        return this.viewAction;
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
