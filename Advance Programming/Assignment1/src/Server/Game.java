package Server;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

public class Game {
    private final List<ClientAccount> mapAccounts = new ArrayList<>();
    private int customerId;
    private boolean hasBall;

    public void setUpClient(int customerId, boolean hasBall)
    {   this.customerId=customerId;
        ClientAccount clientAccount = new ClientAccount(customerId,hasBall);
        clientAccount.setBall(hasBall);
        mapAccounts.add(clientAccount);
    }
    public int getCustomerID(){
        return customerId;
    }
//    public int getCustomerAccount(){
//        int accountNumber=1000;
//        for(ClientAccount acc:mapAccounts.values()){
//            if(acc.getAccountNumber()>=accountNumber){
//                accountNumber++;
//            }
//        }
//        return accountNumber;
//    }

    public List<ClientAccount> getListOfClient() {
        List<ClientAccount> result = mapAccounts;

//        for (ClientAccount clientAccount : mapAccounts)
//            if (clientAccount.getClientId() == customerId)
//                result.add(clientAccount.getAccountNumber());

        return result;
    }

//    public boolean getAccountBalance(int customerId, int accountNumber) throws Exception {
//        if (mapAccounts.get(accountNumber).getCustomerId() != customerId)
//            throw new Exception("Account " + accountNumber + " + belongs to a different customer; customer " + customerId + " is not authorised to query balance for this account.");
//
//        return mapAccounts.get(accountNumber).getBall();
//    }

    //allow client to switch the ball position among them
    public void transfer(int fromAccount, int toAccount, boolean ball) throws Exception {
        synchronized (mapAccounts)
        {
//            if (accounts.get(fromAccount).getCustomerId() != customerId)
//                throw new Exception("Account " + fromAccount + " belongs to a different customer; customer " + customerId + " is not authorised to transfer from this account.");
//            if (accounts.get(fromAccount).getBall() < amount)
//                throw new Exception(
//                        "The balance of account " + fromAccount + " is " + accounts.get(fromAccount).getBalance() + " which is insufficient to transfer " + amount + ".");
//            if (amount <= 0)
//                throw new Exception("Transfer amount has to be a positive value.");
            mapAccounts.get(fromAccount).setBall(mapAccounts.get(fromAccount).getBall() == ball);
            mapAccounts.get(toAccount).setBall(mapAccounts.get(toAccount).getBall()==ball);
        }
    }
}
