import javax.swing.*;
import java.awt.*;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;

public class Client extends JFrame   {

    private JPanel panel1;
    private JButton btnConnect;
    private JButton btnPassBall;
    private JButton btndisconnect;
    private JTextPane liveMonitor;
    private JScrollPane activeClientPan;
    private JLabel lblActivelist;
    private JLabel lblMonitor;
    final int PORT =8888;

    public void setUpClient() throws IOException {
        String sentence = null;
        String modifiedSentence;
        BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));
        Socket clientSocket = null;
        try {
            clientSocket = new Socket("localhost", PORT);
        } catch (IOException e) {
            e.printStackTrace();
        }
        DataOutputStream outToServer = null;
        try {
            outToServer = new DataOutputStream(clientSocket.getOutputStream());
        } catch (IOException e) {
            e.printStackTrace();
        }
        BufferedReader inFromServer = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
        try {
            sentence = inFromUser.readLine();
        } catch (IOException e) {
            e.printStackTrace();
        }
        outToServer.writeBytes(sentence + 'n');
        modifiedSentence = inFromServer.readLine();
        System.out.println("FROM SERVER: " + modifiedSentence);
        clientSocket.close();

    }
    public void createUIComponents() {
        JFrame clientFrame =new JFrame();
        clientFrame.add(panel1);
        clientFrame.add(lblMonitor);
        clientFrame.add(lblActivelist);
        clientFrame.add(liveMonitor);
        clientFrame.add(activeClientPan);


        clientFrame.add(btnConnect);
        clientFrame.add(btndisconnect);
        clientFrame.add(btnPassBall);
        clientFrame.pack();
        clientFrame.setSize(500,500);


        clientFrame.setVisible(true);
        clientFrame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);

    }

}
