package models;

import exceptions.KuantumCokusuException;
import java.lang.String;

public abstract class KuantumNesnesi {
    // Özellikler (Private ile Kapsülleme)
    private String ID;
    private int tehlikeSeviyesi;
    private double stabilite;

    // Constructor
    public KuantumNesnesi(String id, int tehlikeSeviyesi, double baslangicStabilite) {
        this.ID = id;
        this.tehlikeSeviyesi = tehlikeSeviyesi;
        // Setter metodunu kullanarak ilk atamayı yapıyoruz.
        setStabilite(baslangicStabilite); 
    }

    // Kapsülleme: Getter Metotları
    public String getID() { return ID; }
    public int getTehlikeSeviyesi() { return tehlikeSeviyesi; }
    public double getStabilite() { return stabilite; }

    // Kapsülleme: Stabiliteyi kontrol eden Setter Metodu
    protected void setStabilite(double value) {
        if (value > 100) {
            this.stabilite = 100.0;
        } else if (value <= 0) {
            this.stabilite = 0.0;
        } else {
            this.stabilite = value;
        }
    }
    
    // Yardımcı Metot: Stabilitesi düşüşü kontrol eder ve çöküşü fırlatır.
    protected void azaltVeKontrolEt(double dususMiktari) {
        if (this.stabilite - dususMiktari <= 0) {
            throw new KuantumCokusuException(this.ID);
        }
        setStabilite(this.stabilite - dususMiktari);
    }

    // Soyut Metot
    public abstract void AnalizEt();

    // Durum Bilgisi Metodu
    public String DurumBilgisi() {
        return String.format("[ID: %s] Stabilite: %.2f%%, Tehlike: %d", 
            ID, stabilite, tehlikeSeviyesi);
    }
}