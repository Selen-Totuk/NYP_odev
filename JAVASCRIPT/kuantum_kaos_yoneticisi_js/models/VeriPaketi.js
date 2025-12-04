// models/VeriPaketi.js
const KuantumNesnesi = require('./KuantumNesnesi');

class VeriPaketi extends KuantumNesnesi {
    constructor(id) {
        super(id, 1, 80); 
    }

    AnalizEt() {
        this._azaltVeKontrolEt(5);
        console.log(`[VeriPaketi - ${this.ID}] Veri içeriği okundu. Stabilite -5.`);
    }
}
module.exports = VeriPaketi;