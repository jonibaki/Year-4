package Server;

public class ClientAccount {
    private final int clientId;
    private boolean ball;
    private boolean hasBall;

    public ClientAccount(int clientId, boolean hasBall) {
        this.clientId = clientId;
        this.hasBall = hasBall;
    }

    public int getClientId() {
        return clientId;
    }

    public boolean getBall() {
        return ball;
    }

    public void setBall(boolean gameBall) {
        ball = gameBall;
    }
}
