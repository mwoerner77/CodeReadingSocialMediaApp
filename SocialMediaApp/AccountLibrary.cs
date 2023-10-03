namespace SocialMediaApp
{
    using SocialMediaApp.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    public class AccountLibrary
    {
        private List<Account?>? accounts = new List<Account?>();
        private static AccountLibrary accountLibraryInstance = new AccountLibrary();

        //29:
        //
        private AccountLibrary()
        { 
        }

        //30:
        //
        public static AccountLibrary GetAccountLibrarySingleInstance()
        {
            return accountLibraryInstance;
        }

        public bool TryGetAccount(string username, out Account? account)
        { 
            //31:
            //
            this.FetchAllAccounts();

            //32:
            //
            if (this.accounts != null)
            {
                //33:
                //
                foreach (Account? acc1 in this.accounts)
                {
                    if (acc1 != null && acc1.Username != null && acc1.Username == username)
                    {
                        account = acc1;
                        return true;
                    }
                }
            }

            account = null;
            return false;
        }

        public void AddNewAccount(string username, int password)
        {
            //35:
            //
            Account newAccount = new Account()
            {
                Username = username,
                Password = password.ToString()
            };

            //36:
            //
            this.FetchAllAccounts();

            //37:
            //
            this.accounts?.Add(newAccount);
            string accountsJsonText = JsonSerializer.Serialize(this.accounts);
            File.WriteAllText("Accounts.json", accountsJsonText);
        }

        public void AddNewAccount(string username, string password)
        {
            //39:
            //
            Account newAccount = new Account()
            {
                Username = username,
                Password = password
            };

            //40:
            //
            this.FetchAllAccounts();

            //41:
            //
            this.accounts?.Add(newAccount);
            string accountsJsonText = JsonSerializer.Serialize(this.accounts);
            File.WriteAllText("Accounts.json", accountsJsonText);
        }

        //43:
        //
        private void FetchAllAccounts()
        {
            string accountsJsonText = File.ReadAllText("Accounts.json");
            this.accounts = JsonSerializer.Deserialize<List<Account?>>(accountsJsonText);
        }
    }
}
