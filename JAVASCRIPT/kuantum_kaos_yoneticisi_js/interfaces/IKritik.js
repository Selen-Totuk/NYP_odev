// interfaces/IKritik.js
// Javascript'te resmi arayüz olmadığı için, bir nesnenin gerekli metoda sahip olup olmadığını kontrol ederiz.
function isIKritik(nesne) {
    // Sadece AcilDurumSogutmasi metoduna sahip olup olmadığını kontrol eder.
    return nesne && typeof nesne.AcilDurumSogutmasi === 'function';
}
module.exports = { isIKritik };