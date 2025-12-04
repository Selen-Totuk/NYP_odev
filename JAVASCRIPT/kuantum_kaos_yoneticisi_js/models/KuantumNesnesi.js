// models/KuantumNesnesi.js
const KuantumCokusuError = require('../exceptions/KuantumCokusuError');

class KuantumNesnesi {
    // Kapsülleme: Private Alan (#stabilite)
    #stabilite; 

    constructor(id, tehlikeSeviyesi, baslangicStabilite) {
        // Abstract kontrolü: Doğrudan örneklendirmeyi engeller
        if (new.target === KuantumNesnesi) {
            throw new TypeError("KuantumNesnesi soyut bir sınıftır ve doğrudan örneklendirilemez.");
        }
        
        this.ID = id;
        this.TehlikeSeviyesi = tehlikeSeviyesi;
        this.stabilite = baslangicStabilite; // Setter'ı çağırır
    }

    // Kapsülleme: Getter metodu
    get stabilite() {
        return this.#stabilite;
    }

    // Kapsülleme: Setter metodu (0-100 aralığı kontrolü)
    set stabilite(value) {
        if (value > 100) {
            this.#stabilite = 100;
        } else if (value <= 0) {
            this.#stabilite = 0;
        } else {
            this.#stabilite = value;
        }
    }

    // Soyut Metot taklidi (Alt sınıflar uygulamalı)
    AnalizEt() {
        throw new Error("AnalizEt metodu alt sınıflarda uygulanmalıdır.");
    }

    DurumBilgisi() {
        return `[ID: ${this.ID}] Stabilite: ${this.stabilite.toFixed(2)}%, Tehlike: ${this.TehlikeSeviyesi}`;
    }
    
    // Yardımcı Metot
    _azaltVeKontrolEt(dususMiktari) {
        if (this.#stabilite - dususMiktari <= 0) {
            throw new KuantumCokusuError(this.ID);
        }
        this.stabilite = this.#stabilite - dususMiktari;
    }
}
module.exports = KuantumNesnesi;