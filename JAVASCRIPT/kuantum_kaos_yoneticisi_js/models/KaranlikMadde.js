// models/KaranlikMadde.js
const KuantumNesnesi = require('./KuantumNesnesi');

class KaranlikMadde extends KuantumNesnesi {
    constructor(id) {
        super(id, 7, 60); 
    }

    AnalizEt() {
        this._azaltVeKontrolEt(15);
        console.log(`[KaranlikMadde - ${this.ID}] Radyasyon seviyesi yükseldi. Stabilite -15.`);
    }

    // IKritik arayüz metodu
    AcilDurumSogutmasi() {
        this.stabilite += 50; 
        console.log(`[KaranlikMadde - ${this.ID}] Acil Soğutma Başarılı! Stabilite +50.`);
    }
}
module.exports = KaranlikMadde;