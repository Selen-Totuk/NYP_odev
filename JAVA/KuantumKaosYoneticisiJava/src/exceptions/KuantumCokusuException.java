package exceptions;

public class KuantumCokusuException extends RuntimeException {
    // Patlayan nesnenin ID'sini tutan alan
    private String patlayanNesneID; 

    public KuantumCokusuException(String nesneId) {
        // Hata mesajını ana sınıfa ilet (Game Over mesajının bir parçası)
        super("Kuantum Çöküşü! Patlayan Nesne ID: " + nesneId);
        this.patlayanNesneID = nesneId;
    }
    
   
    public String getPatlayanNesneID() {
        return patlayanNesneID;
    }
}