# main.py
import random
from nesneler import KuantumNesnesi, VeriPaketi, KaranlikMadde, AntiMadde
from exceptions import KuantumCokusuException, IKritik

envanter = []
nesne_sayaci = 1

def yeni_nesne_ekle():
    global nesne_sayaci
    id_str = f"QN-{nesne_sayaci:03d}"
    nesne_sayaci += 1
    
    tip = random.randint(1, 3) # 1: Veri, 2: Karanlık, 3: Anti

    if tip == 1:
        yeni_nesne = VeriPaketi(id_str)
    elif tip == 2:
        yeni_nesne = KaranlikMadde(id_str)
    else:
        yeni_nesne = AntiMadde(id_str)

    envanter.append(yeni_nesne)
    print(f"\n[BAŞARILI] Yeni nesne eklendi: {type(yeni_nesne).__name__} - ID: {id_str}")

def tum_envanteri_listele():
    if not envanter:
        print("Envanterde hiç nesne yok.")
        return

    print("\n--- ENVANTER DURUM RAPORU ---")
    # Polimorfizm: Tüm nesnelerin DurumBilgisi() metodu çağrılır.
    for nesne in envanter:
        print(nesne.DurumBilgisi() + f" (Tür: {type(nesne).__name__})")

def nesneyi_analiz_et():
    id_input = input("Analiz edilecek nesnenin ID'sini girin: ").strip()
    
    # Nesneyi ID'ye göre bulma
    hedef = next((n for n in envanter if n.ID.lower() == id_input.lower()), None)

    if hedef:
        # Polimorfizm: Nesnenin gerçek tipine göre AnalizEt() metodu çalışır.
        hedef.AnalizEt()
        print(f"Analiz sonrası durum: {hedef.DurumBilgisi()}")
    else:
        print("HATA: Belirtilen ID'ye sahip nesne bulunamadı.")

def acil_durum_sogutmasi_yap():
    id_input = input("Soğutulacak nesnenin ID'sini girin: ").strip()

    hedef = next((n for n in envanter if n.ID.lower() == id_input.lower()), None)

    if hedef is None:
        print("HATA: Belirtilen ID'ye sahip nesne bulunamadı.")
        return

    # Type Checking (isinstance): Nesnenin IKritik olup olmadığı kontrol edilir.
    if isinstance(hedef, IKritik):
        # Eğer IKritik ise AcilDurumSogutmasi() metodu çağrılabilir.
        hedef.AcilDurumSogutmasi()
        print(f"Soğutma sonrası durum: {hedef.DurumBilgisi()}")
    else:
        # VeriPaketi (IKritik olmayan) için hata mesajı
        print("HATA: Bu nesne (VeriPaketi) kritik bir nesne değildir ve soğutulamaz!")

def main_loop():
    while True:
        try:
            print("\n--- KUANTUM AMBARI KONTROL PANELİ ---")
            print("1. Yeni Nesne Ekle")
            print("2. Tüm Envanteri Listele (Durum Raporu)")
            print("3. Nesneyi Analiz Et (ID isteyerek)")
            print("4. Acil Durum Soğutması Yap")
            print("5. Çıkış")
            
            secim = input("Seçiminiz: ").strip()

            if secim == '1':
                yeni_nesne_ekle()
            elif secim == '2':
                tum_envanteri_listele()
            elif secim == '3':
                nesneyi_analiz_et()
            elif secim == '4':
                acil_durum_sogutmasi_yap()
            elif secim == '5':
                print("Program sonlandırılıyor...")
                break
            else:
                print("Geçersiz seçim. Lütfen 1-5 arasında bir rakam girin.")

        # Game Over: KuantumCokusuException yakalanırsa program sonlanır.
        except KuantumCokusuException as ex:
            print("\n**************************************************")
            print("!!!!!!!!!!!! SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... !!!!!!!!!!!!")
            print(f"Hata: {ex}")
            print("**************************************************")
            break # Döngüyü sonlandır
        except Exception as ex:
            print(f"Beklenmedik bir hata oluştu: {ex}")

if __name__ == "__main__":
    main_loop()