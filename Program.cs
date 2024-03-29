using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;


namespace NethereumSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            // uncomment the function to be run!
            //GetValue().Wait();
            //SetValue().Wait();
            Console.ReadLine();
        }


        static async Task SetValue()
        {
            // Setup the account and "wallet-like" portion
            var url = ""; // Insert Infura API Key for corresponding network
            var privateKey = ""; // Insert Private Key
            var account = new Account(privateKey, 421614); // Second param is Arbitrum Sepolia ChainID
            var web3 = new Web3(account, url);

            // Setups the contract and function to be interacted with via variables
            string contractAddress = "0xd8ff1F4C7F1cfE21A66B7361D5aF3838FC7a5bb2";
            string contractABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getValue\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"setValue\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"totalValue\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";

            var contract = web3.Eth.GetContract(contractABI, contractAddress);
            var setValueFunction = contract.GetFunction("setValue");
            uint valueToSet = 512;

            // Actually interacts with the contract's function
            var transactionReceipt = await setValueFunction.SendTransactionAndWaitForReceiptAsync(account.Address, new HexBigInteger(500000), null, null, valueToSet);

            string transactionHash = transactionReceipt.TransactionHash;
            Console.WriteLine("Transaction sent. Transaction Hash: " + transactionHash);
        }




        static async Task GetValue()
        {
            // Setup the account and "wallet-like" portion
            var url = ""; // Insert Infura API Key for corresponding network
            var privateKey = ""; // Insert Private Key
            var account = new Account(privateKey, 421614); // Second param is Arbitrum Sepolia ChainID
            var web3 = new Web3(account, url);

            // Setups the contract and function to be interacted with via variables
            string contractAddress = "0xd8ff1F4C7F1cfE21A66B7361D5aF3838FC7a5bb2";
            string contractABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getValue\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"setValue\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"totalValue\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";

            var contract = web3.Eth.GetContract(contractABI, contractAddress);
            var getValueFunction = contract.GetFunction("getValue");


            // Actually interacts with the contract's function
            var value = await getValueFunction.CallAsync<uint>();

            Console.WriteLine("Value retrieved from contract: " + value);
        }
    }
}