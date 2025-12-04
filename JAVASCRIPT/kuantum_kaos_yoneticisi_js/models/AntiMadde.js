// models/AntiMadde.js
const KuantumNesnesi = require('./KuantumNesnesi');

class AntiMadde extends KuantumNesnesi {
    constructor(id) {
        super(id, 10, 40); 
    }

    AnalizEt() {
        this._azaltVeKontrolEt(25);
        console.log(`[AntiMadde - ${this.ID}] EVRENİN DOKUSU TİTRİYOR... Stabilite -25.`);
    }

    // IKritik arayüz metodu
    AcilDurumSogutmasi() {
        this.stabilite += 50;
        console.log(`[AntiMadde - ${this.ID}] KRİTİK SOĞUTMA! Stabilite +50. Başarılı.`);
    }
}
module.exports = AntiMadde;