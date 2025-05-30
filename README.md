# Python Selenium POC

Bu proje, Python ve Selenium kullanarak web otomasyon testleri yazmak için hazırlanmış bir örnek projedir. Proje, temel test senaryolarını ve yardımcı fonksiyonları içermektedir.

## Klasör Yapısı

- `MainTestHelper.py`: Testler için temel yardımcı sınıf ve fonksiyonlar.
- `Random.py`: Rastgele veri üretimi için yardımcı sınıf.
- `test.py`: Test senaryolarının tanımlandığı ana dosya.
- `scripts/`: Test çalıştırmaları sırasında oluşan log ve ekran görüntüleri.
- `vite-project/`: Test edilen örnek web uygulaması (React tabanlı).

## Kurulum

1. Gerekli Python paketlerini yükleyin:
   ```sh
   pip install selenium
   ```

2. [ChromeDriver](https://chromedriver.chromium.org/downloads) veya kullandığınız tarayıcıya uygun WebDriver'ı indirin ve sistem PATH'ine ekleyin.

3. Web uygulamasını başlatmak için:
   ```sh
   cd vite-project
   npm install
   npm run dev
   ```

## Testleri Çalıştırma

Ana test dosyası olan `test.py` içindeki ilgili test sınıfının yorumunu kaldırarak çalıştırabilirsiniz. Örneğin:

```python
if __name__ == "__main__":
    #SayfadaVarMiTest().TesteBasla()
    #GizlenmesiniBekleTest().TesteBasla()
    #GorunmesiniBekleTest().TesteBasla()
    #CheckOptionTest().TesteBasla()
    #AcilirKutudaSecTest().TesteBasla()
    ImleciGoturTest().TesteBasla()
```

Birden fazla testi çalıştırmak için ilgili satırların başındaki `#` işaretini kaldırın.

Testi başlatmak için:
```sh
python test.py
```

## Notlar

- `Random.py` dosyasını kullanırken dosya adının büyük harfle başladığına dikkat edin.
- Testler, `http://localhost:5173/` adresinde çalışan örnek web uygulamasını hedefler.
- Hatalar ve loglar `scripts/log.txt` dosyasına kaydedilir.

## Katkı

Katkıda bulunmak için lütfen bir pull request gönderin veya issue açın.

---

Herhangi bir sorunuz olursa lütfen iletişime geçin.