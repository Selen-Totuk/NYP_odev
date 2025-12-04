# nesneler.py
from abc import ABC, abstractmethod
from exceptions import KuantumCokusuException, IKritik

# A. Temel Yapı: KuantumNesnesi (Abstract Class & Encapsulation)
class KuantumNesnesi(ABC):
    def __init__(self, id, tehlike_seviyesi, baslangic_stabilite):
        self.ID = id
        self.TehlikeSeviyesi = tehlike_seviyesi
        
        # Stabilite özelliğini kapsüllemek için özel değişken
        self._stabilite = 0.0
        # Kapsüllemeyi tetiklemek için setter'ı çağır
        self.stabilite = baslangic_stabilite 

    # Kapsülleme: Stabilite'nin değerini almak için getter metodu
    @property
    def stabilite(self):
        return self._stabilite

    # Kapsülleme: Stabilite'nin 0-100 aralığında kalmasını sağlayan setter metodu
    @stabilite.setter
    def stabilite(self, value):
        if value > 100:
            self._stabilite = 100.0
        elif value <= 0:
            self._stabilite = 0.0
        else:
            self._stabilite = value

    # Soyut Metot: Alt sınıfların uygulaması zorunlu
    @abstractmethod
    def AnalizEt(self):
        pass

    # Durum Bilgisi Metodu
    def DurumBilgisi(self):
        return f"[ID: {self.ID}] Stabilite: {self.stabilite:.2f}%, Tehlike: {self.TehlikeSeviyesi}"

    def _azalt_ve_kontrol_et(self, dusus_miktari):
        """Stabiliteyi düşüren ve çöküş kontrolü yapan yardımcı metot."""
        if self.stabilite - dusus_miktari <= 0:
            raise KuantumCokusuException(self.ID)
        
        # Setter metodu sayesinde 0'ın altına düşmeyecek
        self.stabilite -= dusus_miktari

# C. Nesne Çeşitleri: Kalıtım (Inheritance)

class VeriPaketi(KuantumNesnesi):
    def __init__(self, id):
        # Tehlike 1, Stabilite 80
        super().__init__(id, 1, 80)

    def AnalizEt(self):
        self._azalt_ve_kontrol_et(5) # Stabilite 5 birim düşer.
        print(f"[VeriPaketi - {self.ID}] Veri içeriği okundu. Stabilite -5.")

class KaranlikMadde(KuantumNesnesi, IKritik): # IKritik işaretleyicisi uygulanır
    def __init__(self, id):
        # Tehlike 7, Stabilite 60
        super().__init__(id, 7, 60)

    def AnalizEt(self):
        self._azalt_ve_kontrol_et(15) # Stabilite 15 birim düşer.
        print(f"[KaranlikMadde - {self.ID}] Radyasyon seviyesi yükseldi. Stabilite -15.")

    # IKritik arayüz metodu
    def AcilDurumSogutmasi(self):
        self.stabilite += 50 # Stabilite setter'ı sayesinde Max 100
        print(f"[KaranlikMadde - {self.ID}] Acil Soğutma Başarılı! Stabilite +50.")

class AntiMadde(KuantumNesnesi, IKritik): # IKritik işaretleyicisi uygulanır
    def __init__(self, id):
        # Tehlike 10, Stabilite 40
        super().__init__(id, 10, 40)

    def AnalizEt(self):
        self._azalt_ve_kontrol_et(25) # Stabilite 25 birim düşer.
        print(f"[AntiMadde - {self.ID}] EVRENİN DOKUSU TİTRİYOR... Stabilite -25.")

    # IKritik arayüz metodu
    def AcilDurumSogutmasi(self):
        self.stabilite += 50 # Stabilite setter'ı sayesinde Max 100
        print(f"[AntiMadde - {self.ID}] KRİTİK SOĞUTMA! Stabilite +50. Başarılı.")