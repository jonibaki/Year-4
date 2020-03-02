package Client.Client;

import java.util.Scanner;

public class ClientProgram {
    public static void main(String[] args) {

        //System.out.println("Enter your customer ID:");
        System.out.println("Type \"Connect\" join the server:");

        try {
            Scanner in = new Scanner(System.in);
            String clientRequest = in.nextLine();

            try (Client client = new Client(clientRequest)) {
                System.out.println("Connected to the server Successfully!");

                while (true) {
//                    int [] accountNumbers = client.getAccountNumbers();
//                    System.out.println("Your accounts:");
//                    for (int account : accountNumbers)
//                        System.out.printf("  Account %5d: balance %10d GBP%n", account, client.getBalance(account));

                    //TODO: Make a rule here for client to pass the ball to himself
                    // or select other client from the given client lists to pass to them
                    // Set a boolean which could determine the next set of algorithm
                    //System.out.println("Choose between creating an account (C) and transfer (T):");

                    //if client hasBall then
                    //type client name or their client ID to pass the ball

                    System.out.println("Currently you have the ball");
                    System.out.println("Choose between passing the ball to yourself(O) or another active client (T):");

                    String choice = in.nextLine().trim().toUpperCase();
                    switch (choice) {
                        case "O":
                            int newAccount = client.setConnection();
                            System.out.println("Account " + newAccount + " created.");
                            break;

                        case "T":
                            System.out.println("Enter the account number to transfer from or -1 to print the account list:");
                            int fromAccount = client.getClientID();
                            if (fromAccount < 0)
                                continue;

                            System.out.println("Enter the Client ID to pass the ball: ");
                            int toAccount = Integer.parseInt(in.nextLine());

                            System.out.println("Enter the client to pass the ball:");
                            //System.out.println("Enter the amount to be transferred:");
                            //int amount = Integer.parseInt(in.nextLine());
                            boolean ball=  client.getBall();

                            client.transfer(fromAccount, toAccount, ball);
                            break;

                        default:
                            System.out.println("Unknown command: " + choice);
                            break;
                    }
                }
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
