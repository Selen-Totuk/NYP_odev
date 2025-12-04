// exceptions/KuantumCokusuError.js
class KuantumCokusuError extends Error {
    constructor(nesneId) {
        super(`Kuantum Çöküşü! Patlayan Nesne ID: ${nesneId}`);
        this.name = 'KuantumCokusuError';
        this.patlayanNesneID = nesneId;
    }
}
module.exports = KuantumCokusuError;