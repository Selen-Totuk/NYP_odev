package models;

import interfaces.IKritik;

public class KaranlikMadde extends KuantumNesnesi implements IKritik {
    
    public KaranlikMadde(String id) {
        super(id, 7, 60); 
    }

    @Override
    public void AnalizEt() {
        azaltVeKontrolEt(15); // Stabilite 15 birim düşer.
        System.out.println("[KaranlikMadde - " + getID() + "] Radyasyon seviyesi yükseldi. Stabilite -15.");
    }

    @Override
    public void AcilDurumSogutmasi() {
        setStabilite(getStabilite() + 50); 
        System.out.println("[KaranlikMadde - " + getID() + "] Acil Soğutma Başarılı! Stabilite +50.");
    }
}