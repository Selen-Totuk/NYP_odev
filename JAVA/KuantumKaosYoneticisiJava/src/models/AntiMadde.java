package models;

import interfaces.IKritik;

public class AntiMadde extends KuantumNesnesi implements IKritik {
    
    public AntiMadde(String id) {
        super(id, 10, 40); 
    }

    @Override
    public void AnalizEt() {
        azaltVeKontrolEt(25); // Stabilite 25 birim düşer.
        System.out.println("[AntiMadde - " + getID() + "] EVRENİN DOKUSU TİTRİYOR... Stabilite -25.");
    }

    @Override
    public void AcilDurumSogutmasi() {
        setStabilite(getStabilite() + 50);
        System.out.println("[AntiMadde - " + getID() + "] KRİTİK SOĞUTMA! Stabilite +50. Başarılı.");
    }
}