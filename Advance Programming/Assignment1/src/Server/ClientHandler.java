package Server;

import java.io.PrintWriter;
import java.net.Socket;
import java.util.List;
import java.util.Scanner;

public class ClientHandler implements Runnable {
    private final Socket socket;
    private Game game;

    public ClientHandler(Socket socket, Game game) {
        this.socket = socket;
        this.game = game;
    }

    @Override
    public void run() {
        int clientUniqueId = 0;
        boolean ball=false;
        String request="CONNECT";
        try (
                Scanner scanner = new Scanner(socket.getInputStream());
                PrintWriter writer = new PrintWriter(socket.getOutputStream(), true))
        {
            try {
                if(request.equalsIgnoreCase(scanner.nextLine())){
                    clientUniqueId++;
                    System.out.println("New connection; customer ID " + clientUniqueId);
                    //writer.println("New connection; customer ID " + clientUniqueId);

                    //first client always get the ball
                    if(game.getListOfClient().size()==0){
                        ball=true;
                    }
                    game.setUpClient(clientUniqueId,ball);
                    writer.println("SUCCESS");

                    while (true) {
                        String line = scanner.nextLine();
                        String[] substrings = line.split(" ");
                        switch (substrings[0].toLowerCase()) {
                        case "clients":
                            List<ClientAccount> listOfClient = game.getListOfClient();
                            writer.println(listOfClient.size());
                            for (ClientAccount numberClient : listOfClient)
                                writer.println(numberClient);
                            break;
//
//                        case "balance":
//                            int account = Integer.parseInt(substrings[1]);
//                            writer.println(game.getAccountBalance(customerId, account));
//                            break;

                            case "transfer":
                                int fromAccount = game.getCustomerID();
                                int toAccount = Integer.parseInt(substrings[2]);
                                boolean tempBall= true;
                                //int amount=Integer.parseInt(substrings[3]);
                                game.transfer(fromAccount, toAccount, tempBall);
                                writer.println("SUCCESS");
                                break;
//                        case "connect":
//                            int accNumber= game.getCustomerAccount();
//                            game.createAccount(game.getCustomerID(),accNumber,false);
//                            writer.println("New Account: "+accNumber );
//
//                            break;
                            default:
                                throw new Exception("Unknown command: " + substrings[0]);
                        }
                }

                }
            } catch (Exception e) {
                writer.println("ERROR " + e.getMessage());
                socket.close();
            }
        } catch (Exception e) {
        } finally {
            System.out.println("Customer " + clientUniqueId + " disconnected.");
        }


//                if (game.getListOfAccounts(customerId).size() == 0)
//                    throw new Exception("Unknown customer: " + customerId + ".");

    }
}
