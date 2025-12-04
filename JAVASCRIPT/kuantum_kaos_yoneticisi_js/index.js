// index.js
const readline = require('readline');
const KuantumCokusuError = require('./exceptions/KuantumCokusuError');
const { isIKritik } = require('./interfaces/IKritik');
const VeriPaketi = require('./models/VeriPaketi');
const KaranlikMadde = require('./models/KaranlikMadde');
const AntiMadde = require('./models/AntiMadde');

const envanter = [];
let nesneSayaci = 1;

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

const getNesneById = (id) => {
    return envanter.find(n => n.ID.toLowerCase() === id.toLowerCase());
};

function yeniNesneEkle() {
    const id = `QN-${String(nesneSayaci++).padStart(3, '0')}`;
    let yeniNesne;
    const tip = Math.floor(Math.random() * 3); // 0, 1, 2

    switch (tip) {
        case 0: yeniNesne = new VeriPaketi(id); break;
        case 1: yeniNesne = new KaranlikMadde(id); break;
        case 2: yeniNesne = new AntiMadde(id); break;
        default: return;
    }
    envanter.push(yeniNesne);
    console.log(`\n[BAŞARILI] Yeni nesne eklendi: ${yeniNesne.constructor.name} - ID: ${id}`);
}

function tumEnvanteriListele() {
    if (envanter.length === 0) {
        console.log("Envanterde hiç nesne yok.");
        return;
    }
    console.log("\n--- ENVANTER DURUM RAPORU ---");
    // Polimorfizm: DurumBilgisi() çağrılır
    envanter.forEach(nesne => {
        console.log(nesne.DurumBilgisi() + ` (Tür: ${nesne.constructor.name})`);
    });
}

function nesneyiAnalizEt(callback) {
    rl.question("Analiz edilecek nesnenin ID'sini girin: ", (id) => {
        const hedef = getNesneById(id.trim());
        if (hedef) {
            try {
                hedef.AnalizEt();
                console.log(`Analiz sonrası durum: ${hedef.DurumBilgisi()}`);
            } catch (error) {
                if (error instanceof KuantumCokusuError) {
                    throw error; // Game Over için ana döngüye fırlat
                }
                console.error(`Analiz sırasında beklenmedik hata: ${error.message}`);
            }
        } else {
            console.log("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
        }
        callback(); // İşlem bitince ana döngüyü tekrar çağır
    });
}

function acilDurumSogutmasiYap(callback) {
    rl.question("Soğutulacak nesnenin ID'sini girin: ", (id) => {
        const hedef = getNesneById(id.trim());

        if (!hedef) {
            console.log("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
        } else if (isIKritik(hedef)) { // Type Checking (IKritik kontrolü)
            hedef.AcilDurumSogutmasi();
            console.log(`Soğutma sonrası durum: ${hedef.DurumBilgisi()}`);
        } else {
            // VeriPaketi (IKritik olmayan) için hata mesajı
            console.log("HATA: Bu nesne (VeriPaketi) kritik bir nesne değildir ve soğutulamaz!");
        }
        callback();
    });
}

function mainLoop() {
    console.log("\n--- KUANTUM AMBARI KONTROL PANELİ ---");
    console.log("1. Yeni Nesne Ekle");
    console.log("2. Tüm Envanteri Listele (Durum Raporu)");
    console.log("3. Nesneyi Analiz Et (ID isteyerek)");
    console.log("4. Acil Durum Soğutması Yap");
    console.log("5. Çıkış");

    rl.question("Seçiminiz: ", (secim) => {
        try {
            switch (secim.trim()) {
                case '1': yeniNesneEkle(); mainLoop(); break;
                case '2': tumEnvanteriListele(); mainLoop(); break;
                case '3': nesneyiAnalizEt(mainLoop); break; // Asenkron işlem
                case '4': acilDurumSogutmasiYap(mainLoop); break; // Asenkron işlem
                case '5':
                    console.log("Program sonlandırılıyor...");
                    rl.close();
                    break;
                default:
                    console.log("Geçersiz seçim. Lütfen 1-5 arasında bir rakam girin.");
                    mainLoop();
            }
        } catch (error) {
            // Game Over: KuantumCokusuError yakalanırsa program sonlanır.
            if (error instanceof KuantumCokusuError) {
                console.log("\n**************************************************");
                console.log("!!!!!!!!!!!! SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... !!!!!!!!!!!!");
                console.log(`Hata: ${error.message}`);
                console.log("**************************************************");
                rl.close();
            } else {
                console.error(`Beklenmedik bir hata oluştu: ${error.message}`);
                mainLoop();
            }
        }
    });
}

mainLoop();