package Client.Client;

import java.io.*;
import java.net.Socket;
import java.util.Scanner;

public class Client implements AutoCloseable {
    final int port = 8888;

    private final Scanner reader;
    private final PrintWriter writer;

    public Client(String request ) throws Exception {
        // Connecting to the server and creating objects for communications
        Socket socket = new Socket("localhost", port);
        reader = new Scanner(socket.getInputStream());

        // Automatically flushes the stream with every command
        writer = new PrintWriter(socket.getOutputStream(), true);

        // Sending customer ID
        writer.println(request);

        // Parsing the response
        String line = reader.nextLine();
        if (line.trim().compareToIgnoreCase("success") != 0)
            throw new Exception(line);
    }

    public  int getClientID(){
        String line = reader.nextLine();
        return Integer.parseInt(line);
    }
    public boolean getBall() {
        // Sending command
        writer.println("BALL");

        // Reading the number of accounts
        String ballStatus = reader.nextLine();
//        int numberOfAccounts = Integer.parseInt(line);
//
//        // Reading the account numbers
//        int[] accounts = new int[numberOfAccounts];
//        for (int i = 0; i < numberOfAccounts; i++) {
//            line = reader.nextLine();
//            accounts[i] = Integer.parseInt(line);
//        }


        return ballStatus.equals("true");
    }

    public int getBalance(int accountNumber) {
        // Writing the command
        writer.println("BALANCE " + accountNumber);

        // Reading the account balance
        String line = reader.nextLine();
        return Integer.parseInt(line);
    }

    //TODO: use to transfer the ball to another client
    public void transfer(int fromAccount, int toAccount, boolean ball) throws Exception {
        // Writing the command
        writer.println("PASS BALL " + fromAccount + " " + toAccount + " " + ball);

        // Reading the response
        String line = reader.nextLine();
        if (line.trim().compareToIgnoreCase("success") != 0)
            throw new Exception(line);
    }

    public int setConnection() {
        // Writing the command
        writer.println("SET_CONNECTION");

        // Reading the new account number
        String line = reader.nextLine();
        return Integer.parseInt(line);
    }

    @Override
    public void close() throws Exception {
        reader.close();
        writer.close();
    }
}
