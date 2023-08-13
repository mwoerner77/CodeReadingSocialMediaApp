namespace SocialMediaApp
{
    using SocialMediaApp.Models;

    public class AccountManager
    {
        private AccountLibrary accountLibrary;
        private static AccountManager accountManagerInstance = new AccountManager();

        //44:
        //
        private AccountManager() 
        {
            this.accountLibrary = AccountLibrary.GetAccountLibrarySingleInstance();
        }


        //45:
        //
        public static AccountManager GetAccountManagerSingleInstance()
        {
            return accountManagerInstance;
        }

        public AccountCreationStatus CreateAccount(string username, string password, string confirmedPassword)
        {
            //46:
            //
            if (password != confirmedPassword)
            {
                return AccountCreationStatus.PasswordMismatch;
            }

            //47:
            //
            if (this.accountLibrary.TryGetAccount(username, out Account _))
            {
                return AccountCreationStatus.AlreadyExists;            
            }

            //48:
            //
            this.accountLibrary.AddNewAccount(username, password);
            return AccountCreationStatus.OK;
        }

        public AccountLoginStatus LoginAccount(string username, string password)
        {
            //49:
            //
            if (this.accountLibrary.TryGetAccount(username, out Account? acc))
            {
                //50:
                //
                if (acc?.Password == password)
                {
                    return AccountLoginStatus.OK;
                }
                else
                {
                    return AccountLoginStatus.IncorrectPassword;
                }
            }
            else
            {
                return AccountLoginStatus.DoesNotExist;
            }
        }

        public AccountLoginStatus LoginNewAccount(string username, string password)
        {
            //51:
            //
            if (this.accountLibrary.TryGetAccount(username, out Account? acc))
            {
                //52:
                //
                if (acc?.Password == password)
                {
                    return AccountLoginStatus.OK;
                }
                else
                {
                    return AccountLoginStatus.IncorrectPassword;
                }
            }
            else
            {
                return AccountLoginStatus.DoesNotExist;
            }
        }
    }
}
