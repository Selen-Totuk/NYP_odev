package models;

public class VeriPaketi extends KuantumNesnesi {
    
    public VeriPaketi(String id) {
        super(id, 1, 80); 
    }

    @Override
    public void AnalizEt() {
        azaltVeKontrolEt(5); // Stabilite 5 birim düşer.
        System.out.println("[VeriPaketi - " + getID() + "] Veri içeriği okundu. Stabilite -5.");
    }
}