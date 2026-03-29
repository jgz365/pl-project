using System.Text.Json;
using System.Net;
using inventory_ni_Percie;

namespace customer_kiosk
{
    internal static class CatalogHtmlProvider
    {
        public static string GetHtml()
        {
            bool dbConnected = DatabaseManager.TestConnection();
            var products = DatabaseManager.GetCatalogProducts();

            string dbConnectedJson = dbConnected ? "true" : "false";
            string productsJson = JsonSerializer.Serialize(
                products,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            string productsJsonStringLiteral = JsonSerializer.Serialize(productsJson);
            string initialCardsHtml = BuildInitialCardsHtml(products, dbConnected);

            string html = """
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <style>
    body{font-family:'Inter','Poppins','Montserrat','Roboto','Segoe UI',sans-serif;margin:0;background:#f3f4f6;color:#0f172a}
    .page{display:grid;grid-template-columns:300px 1fr;height:100vh;overflow:hidden}
    .filters{background:#fff;border-right:1px solid #e2e8f0;padding:16px;position:sticky;top:0;height:100vh;overflow:auto}
    .filters h2{margin:0 0 12px;font-size:38px;font-weight:700}
    .f-group{margin-bottom:14px}
    .f-label{display:block;font-size:28px;font-weight:700;color:#475569;margin-bottom:6px}
    .f-select{width:100%;padding:10px 12px;border:1px solid #cbd5e1;border-radius:10px;background:#f3f4f6;font-size:24px;font-weight:700}
    .f-range{width:100%}
    .f-range-row{display:flex;justify-content:space-between;color:#64748b;font-size:22px;font-weight:700;margin-top:4px}
    .brands{display:flex;flex-direction:column;gap:6px;max-height:280px;overflow:auto;padding-right:4px}
    .brand-item{font-size:24px;font-weight:700;display:flex;align-items:center;gap:8px}
    .catalog-content{min-width:0;height:100vh;overflow:auto}
    .wrap{padding:20px;display:grid;grid-template-columns:repeat(auto-fill,minmax(320px,1fr));gap:18px}
    .card{background:#fff;border-radius:14px;padding:14px;box-shadow:0 8px 20px rgba(0,0,0,.08)}
    .img{height:180px;border-radius:10px;background:linear-gradient(135deg,#dbeafe,#cbd5e1);display:flex;align-items:center;justify-content:center;color:#64748b;font-weight:600;overflow:hidden}
    .img img{width:100%;height:100%;object-fit:cover;display:block}
    .title{font-size:22px;font-weight:600;margin:10px 0 2px;letter-spacing:.1px}
    .sub{font-size:13px;font-weight:400;color:#64748b}
    .meta{display:flex;justify-content:space-between;margin:12px 0}
    .price{font-size:30px;font-weight:700}
    .stock{text-align:right}
    .btns{display:flex;gap:10px;margin-top:12px}
    button{border:none;border-radius:10px;padding:12px 14px;cursor:pointer;font-weight:600;font-family:'Inter','Poppins','Montserrat','Roboto',sans-serif}
    .ghost{background:#e5e7eb}
    .dark{background:#0f172a;color:#fff;flex:1}

    .overlay{position:fixed;inset:0;background:rgba(15,23,42,.55);display:none;align-items:center;justify-content:center;padding:20px}
    .modal{background:#fff;border-radius:14px;max-width:980px;width:100%;max-height:92vh;overflow:auto;box-shadow:0 20px 45px rgba(0,0,0,.3)}
    .modal-head{display:flex;justify-content:space-between;align-items:center;padding:16px 18px 0}
    .close{background:transparent;padding:4px 8px;font-size:20px}
    .hero{margin:10px 18px;height:320px;border-radius:10px;background:linear-gradient(135deg,#dbeafe,#cbd5e1);overflow:hidden;display:flex;align-items:center;justify-content:center}
    .hero img{width:100%;height:100%;object-fit:cover;display:block}
    .modal-content{padding:0 18px 18px}
    .section-title{font-size:18px;font-weight:600;margin:18px 0 12px}
    .spec-grid{display:grid;grid-template-columns:1fr 1fr;gap:18px 44px}
    .spec-item{display:flex;gap:10px;align-items:flex-start}
    .spec-icon{font-size:20px;line-height:1;margin-top:2px}
    .spec-label{font-size:14px;font-weight:500;color:#64748b}
    .spec-value{font-size:30px;font-weight:600;color:#0f172a;line-height:1.2}
    .chips{display:flex;gap:10px;flex-wrap:wrap;margin-top:8px}
    .chip{border:1px solid #cbd5e1;background:#eef2f7;color:#0f172a;border-radius:18px;padding:5px 12px;font-size:13px;font-weight:500}
    .desc{font-size:17px;line-height:1.5;color:#64748b;font-weight:400}
    .divider{margin:18px 0;border-top:1px solid #e2e8f0}

    /* scrollbar consistency */
    *{scrollbar-width:thin;scrollbar-color:#cbd5e1 #f8fafc}
    ::-webkit-scrollbar{width:10px;height:10px}
    ::-webkit-scrollbar-track{background:#f8fafc;border-radius:10px}
    ::-webkit-scrollbar-thumb{background:#cbd5e1;border-radius:10px;border:2px solid #f8fafc}
    ::-webkit-scrollbar-thumb:hover{background:#94a3b8}
  </style>
</head>
<body>
  <div class="page">
    <aside class="filters">
      <h2>Filters</h2>
      <div class="f-group">
        <label class="f-label" for="fCategory">Category</label>
        <select id="fCategory" class="f-select">
          <option>All Category</option>
          <option>Sport</option>
          <option>Touring</option>
          <option>Scooter</option>
          <option>Off-Road</option>
          <option>Underbone</option>
          <option>Standard</option>
        </select>
      </div>
      <div class="f-group">
        <label class="f-label" for="fPrice">Price Range</label>
        <input id="fPrice" class="f-range" type="range" min="50000" max="500000" step="10000" value="500000" />
        <div class="f-range-row"><span>₱50,000</span><span id="fPriceValue">₱500,000</span></div>
      </div>
      <div class="f-group">
        <label class="f-label">Brands</label>
        <div class="brands" id="fBrands"></div>
      </div>
      <div class="f-group">
        <label class="f-label" for="fEngine">Engine Size</label>
        <select id="fEngine" class="f-select">
          <option>All Sizes</option>
          <option>Under 100cc</option>
          <option>150cc-300cc</option>
          <option>300cc-500cc</option>
          <option>Over 500cc</option>
        </select>
      </div>
      <div class="f-group">
        <label class="f-label" for="fAvailability">Availability</label>
        <select id="fAvailability" class="f-select">
          <option>All Units</option>
          <option>Available</option>
          <option>Pre-Order</option>
          <option>Limited</option>
        </select>
      </div>
      <div class="f-group">
        <label class="f-label" for="fSort">Sort By</label>
        <select id="fSort" class="f-select">
          <option>Newest First</option>
          <option>Price: Low to High</option>
          <option>Price: High to Low</option>
          <option>Name (A-Z)</option>
        </select>
      </div>
    </aside>
    <main class="catalog-content"><div class="wrap" id="cards">__INITIAL_CARDS__</div></main>
  </div>

  <div class="overlay" id="overlay" onclick="if(event.target===this)hideModal()">
    <div class="modal">
      <div class="modal-head">
        <div>
          <div class="title" id="mTitle">{Product Name}</div>
          <div class="sub" id="mSub">{year} • {Category}</div>
        </div>
        <button class="close" onclick="hideModal()">×</button>
      </div>
      <div class="hero"></div>
      <div class="modal-content">
        <div class="meta">
          <div><div class="sub">Price</div><div class="price" id="mPrice">₱145,000.00</div></div>
          <div class="stock"><div class="sub">Stock</div><div id="mStock">1 units</div></div>
        </div>
        <div class="divider"></div>
        <div class="section-title">Specifications</div>
        <div class="spec-grid">
          <div class="spec-item"><div class="spec-icon">⚡</div><div><div class="spec-label">Max Power</div><div class="spec-value" id="mMaxPower">15.8 hp @ 8,500 rpm</div></div></div>
          <div class="spec-item"><div class="spec-icon">↔</div><div><div class="spec-label">Max Torque</div><div class="spec-value" id="mMaxTorque">14.7 Nm @ 6,500 rpm</div></div></div>
          <div class="spec-item"><div class="spec-icon">⚙</div><div><div class="spec-label">Transmission</div><div class="spec-value" id="mTransmission">CVT Automatic</div></div></div>
          <div class="spec-item"><div class="spec-icon">⛽</div><div><div class="spec-label">Fuel System</div><div class="spec-value" id="mFuelSystem">PGM-FI</div></div></div>
          <div class="spec-item"><div class="spec-icon">❄</div><div><div class="spec-label">Cooling</div><div class="spec-value" id="mCooling">Liquid-cooled</div></div></div>
          <div class="spec-item"><div class="spec-icon">🛢</div><div><div class="spec-label">Fuel Capacity</div><div class="spec-value" id="mFuelCapacity">8.1 liters</div></div></div>
          <div class="spec-item"><div class="spec-icon">↕</div><div><div class="spec-label">Seat Height</div><div class="spec-value" id="mSeatHeight">795 mm</div></div></div>
          <div class="spec-item"><div class="spec-icon">◍</div><div><div class="spec-label">Curb Weight</div><div class="spec-value" id="mCurbWeight">133 kg</div></div></div>
          <div class="spec-item"><div class="spec-icon">⛰</div><div><div class="spec-label">Ground Clearance</div><div class="spec-value" id="mGroundClearance">165 mm</div></div></div>
          <div class="spec-item"><div class="spec-icon">⇄</div><div><div class="spec-label">Wheelbase</div><div class="spec-value" id="mWheelbase">1,324 mm</div></div></div>
          <div class="spec-item"><div class="spec-icon">🛑</div><div><div class="spec-label">Brake System</div><div class="spec-value" id="mBrakeSystem">Front/Rear Disc with ABS</div></div></div>
          <div class="spec-item"><div class="spec-icon">🛠</div><div><div class="spec-label">Suspension</div><div class="spec-value" id="mSuspension">Telescopic Front, Twin Shock Rear</div></div></div>
        </div>

        <div class="section-title" style="margin-top:18px">Available Colors</div>
        <div class="chips" id="mColors"><span class="chip">Matte Black</span></div>

        <div class="section-title" style="margin-top:18px">Features</div>
        <div class="chips" id="mFeatures"><span class="chip">ABS</span><span class="chip">LED Headlights</span><span class="chip">Smart Key</span></div>

        <div class="section-title" style="margin-top:18px">Description</div>
        <p class="desc" id="mDesc">Repossessed unit in good condition. 8 months used. Minor scratches. All documents complete.</p>
        <div class="divider"></div>
        <div class="btns">
          <button class="ghost" onclick="hideModal()">Close</button>
          <button class="dark" onclick="applyNow()">Apply for Loan</button>
        </div>
      </div>
    </div>
  </div>

  <script>
    const data = JSON.parse(__PRODUCT_DATA__);
    const dbConnected = __DB_CONNECTED__;
    const cards = document.getElementById('cards');
    const fCategory = document.getElementById('fCategory');
    const fPrice = document.getElementById('fPrice');
    const fPriceValue = document.getElementById('fPriceValue');
    const fBrands = document.getElementById('fBrands');
    const fEngine = document.getElementById('fEngine');
    const fAvailability = document.getElementById('fAvailability');
    const fSort = document.getElementById('fSort');
    let current = data[0] || null;
    const BRAND_LIST = ['Honda','Yamaha','Kawasaki','Suzuki','KTM','Rusi','Harley-Davidson','CFMOTO','BMW','Ducati'];

    if (!Array.isArray(data) || data.length === 0) {
      const title = dbConnected ? 'No products available' : 'Database connection unavailable';
      const sub = dbConnected
        ? 'Please check product setup in database.'
        : 'Please start XAMPP MySQL and relaunch the app.';
      cards.innerHTML = `<div class='card'><div class='title'>${title}</div><div class='sub'>${sub}</div></div>`;
    }

    function parsePrice(text){
      return Number(String(text || '').replace(/[^0-9.]/g,'')) || 0;
    }

    function parseStock(text){
      const n = parseInt(String(text || '').replace(/[^0-9]/g,''), 10);
      return Number.isFinite(n) ? n : 0;
    }

    function getYear(sub){
      const y = parseInt(String(sub || '').split('•')[0],10);
      return Number.isFinite(y) ? y : 0;
    }

    function getTag(sub){
      return String(sub || '').split('•')[1]?.trim() || '';
    }

    function inferCc(title){
      const m = String(title || '').match(/(\d{2,4})/);
      return m ? parseInt(m[1],10) : 0;
    }

    function getProductCategory(d){
      const text = `${d.title || ''} ${d.desc || ''}`.toLowerCase();
      if (text.includes('scooter') || text.includes('maxi-scooter')) return 'Scooter';
      if (text.includes('underbone')) return 'Underbone';
      if (text.includes('off-road') || text.includes('adventure') || text.includes('dual-purpose')) return 'Off-Road';
      if (text.includes('touring') || text.includes('tour')) return 'Touring';
      if (text.includes('naked') || text.includes('streetfighter') || text.includes('standard')) return 'Standard';
      return 'Sport';
    }

    function matchCategory(d, category){
      if(!category || category === 'All Category') return true;
      return getProductCategory(d).toLowerCase() === category.toLowerCase();
    }

    function matchBrand(d, brand){
      if(!brand) return true;
      const b = brand.toLowerCase();
      return getTag(d.sub).toLowerCase() === b ||
             String(d.title || '').toLowerCase().startsWith(b + ' ');
    }

    function matchEngine(d, engineText){
      const cc = inferCc(d.title);
      if(engineText === 'All Sizes' || !engineText) return true;
      if(engineText === 'Under 100cc') return cc > 0 && cc < 100;
      if(engineText === '150cc-300cc') return cc >= 150 && cc <= 300;
      if(engineText === '300cc-500cc') return cc >= 300 && cc <= 500;
      if(engineText === 'Over 500cc') return cc > 500;
      return true;
    }

    function matchAvailability(d, availabilityText){
      const stock = parseStock(d.stock);
      if(availabilityText === 'All Units' || !availabilityText) return true;
      if(availabilityText === 'Available') return stock > 3;
      if(availabilityText === 'Pre-Order') return stock === 0;
      if(availabilityText === 'Limited') return stock >= 1 && stock <= 3;
      return true;
    }

    function sortData(arr, sortText){
      const mode = sortText || 'Newest First';
      return [...arr].sort((a,b)=>{
        if(mode === 'Price: Low to High') return parsePrice(a.price) - parsePrice(b.price);
        if(mode === 'Price: High to Low') return parsePrice(b.price) - parsePrice(a.price);
        if(mode === 'Name (A-Z)') return String(a.title || '').localeCompare(String(b.title || ''));
        return getYear(b.sub) - getYear(a.sub);
      });
    }

    function renderCards(list){
      cards.innerHTML = '';
      if(list.length === 0){
        cards.innerHTML = `<div class='card'><div class='title'>No matching products</div><div class='sub'>Try adjusting your filters.</div></div>`;
        return;
      }

      list.forEach(d=>{
      const c=document.createElement('div'); c.className='card';
      c.innerHTML=`<div class='img'><img src='${d.imageUrl || "images/products/default.jpg"}' alt='${d.title || "Motorcycle"}' onerror="this.onerror=null;this.src='images/products/default.jpg';"></div><div class='title'>${d.title}</div><div class='sub'>${d.sub}</div><div class='meta'><div><div class='price'>${d.price}</div></div><div class='stock'><div class='sub'>Stock</div>${d.stock}</div></div><div class='btns'><button class='ghost' tabindex='-1'>Details</button><button class='dark' tabindex='-1'>Apply</button></div>`;
      c.querySelector('.ghost').addEventListener('click',()=>showDetails(d));
      c.querySelector('.dark').addEventListener('click',()=>openApplication(d));
      cards.appendChild(c);
      });
    }

    function applyFilters(){
      const brand = document.querySelector('input[name="brandFilter"]:checked')?.value || '';
      const filtered = data.filter(d => {
        if(!matchCategory(d, fCategory.value)) return false;
        if(brand && !matchBrand(d, brand)) return false;
        if(parsePrice(d.price) > Number(fPrice.value || 500000)) return false;
        if(!matchEngine(d, fEngine.value)) return false;
        if(!matchAvailability(d, fAvailability.value)) return false;
        return true;
      });

      renderCards(sortData(filtered, fSort.value));
    }

    function bindFilters(){
      fBrands.innerHTML = BRAND_LIST.map(b=>`<label class='brand-item'><input type='radio' name='brandFilter' value='${b}'> ${b}</label>`).join('');

      // Keep slider ceiling aligned with the highest DB product price (safe for premium units).
      const minPrice = 50000;
      const computedMax = Math.max(minPrice, ...data.map(d => parsePrice(d.price)));
      const normalizedMax = Math.ceil(computedMax / 50000) * 50000;
      fPrice.max = String(normalizedMax);
      fPrice.value = String(normalizedMax);
      fPriceValue.textContent = `₱${Number(fPrice.value).toLocaleString('en-PH')}`;

      fCategory.addEventListener('change', ()=>{
        if (fCategory.value === 'All Category') {
          document.querySelectorAll('input[name="brandFilter"]').forEach(rb => rb.checked = false);
        }

        applyFilters();
      });
      fEngine.addEventListener('change', applyFilters);
      fAvailability.addEventListener('change', applyFilters);
      fSort.addEventListener('change', applyFilters);
      fBrands.addEventListener('change', applyFilters);
      fPrice.addEventListener('input', ()=>{
        fPriceValue.textContent = `₱${Number(fPrice.value).toLocaleString('en-PH')}`;
        applyFilters();
      });
    }

    bindFilters();
    applyFilters();
    function showDetails(d){
      current=d;
      document.querySelector('.hero').innerHTML = `<img src='${d.imageUrl || "images/products/default.jpg"}' alt='${d.title || "Motorcycle"}' onerror="this.onerror=null;this.src='images/products/default.jpg';">`;
      document.getElementById('mTitle').textContent=d.title;
      document.getElementById('mSub').textContent=d.sub;
      document.getElementById('mPrice').textContent=d.price;
      document.getElementById('mStock').textContent=d.stock;
      document.getElementById('mMaxPower').textContent=(d.maxPower||'15.8 hp @ 8,500 rpm');
      document.getElementById('mMaxTorque').textContent=(d.maxTorque||'14.7 Nm @ 6,500 rpm');
      document.getElementById('mTransmission').textContent=(d.transmission||'CVT Automatic');
      document.getElementById('mFuelSystem').textContent=(d.fuelSystem||'PGM-FI');
      document.getElementById('mCooling').textContent=(d.cooling||'Liquid-cooled');
      document.getElementById('mFuelCapacity').textContent=(d.fuelCapacity||'8.1 liters');
      document.getElementById('mSeatHeight').textContent=(d.seatHeight||'795 mm');
      document.getElementById('mCurbWeight').textContent=(d.curbWeight||'133 kg');
      document.getElementById('mGroundClearance').textContent=(d.groundClearance||'165 mm');
      document.getElementById('mWheelbase').textContent=(d.wheelbase||'1,324 mm');
      document.getElementById('mBrakeSystem').textContent=(d.brakeSystem||'Front/Rear Disc with ABS');
      document.getElementById('mSuspension').textContent=(d.suspension||'Telescopic Front, Twin Shock Rear');
      renderChips('mColors', d.colors, ['Matte Black']);
      renderChips('mFeatures', d.features, ['ABS','LED Headlights','Smart Key']);
      document.getElementById('mDesc').textContent=d.desc;
      document.getElementById('overlay').style.display='flex';
    }
    function renderChips(containerId, values, fallback){
      const host = document.getElementById(containerId);
      const items = Array.isArray(values) && values.length > 0 ? values : fallback;
      host.innerHTML = items.map(v=>`<span class='chip'>${v}</span>`).join('');
    }
    function openApplication(product){
      const selected = product || current || null;
      const payload = {
        action: 'open_redirect_application',
        product: selected ? {
          title: selected.title || '',
          sub: selected.sub || '',
          price: selected.price || '',
          imageUrl: selected.imageUrl || ''
        } : null
      };

      window.chrome?.webview?.postMessage(JSON.stringify(payload));
    }
    function hideModal(){ document.getElementById('overlay').style.display='none'; }
    function applyNow(){ openApplication(current); }
  </script>
</body>
</html>
""";

            return html
                .Replace("__PRODUCT_DATA__", productsJsonStringLiteral)
                .Replace("__INITIAL_CARDS__", initialCardsHtml)
                .Replace("__DB_CONNECTED__", dbConnectedJson);
        }

        private static string BuildInitialCardsHtml(IReadOnlyList<DatabaseManager.CatalogProduct> products, bool dbConnected)
        {
            if (products.Count == 0)
            {
                string title = dbConnected ? "No products available" : "Database connection unavailable";
                string sub = dbConnected
                    ? "Please check product setup in database."
                    : "Please start XAMPP MySQL and relaunch the app.";

                return $"<div class='card'><div class='title'>{WebUtility.HtmlEncode(title)}</div><div class='sub'>{WebUtility.HtmlEncode(sub)}</div></div>";
            }

            var sb = new System.Text.StringBuilder();
            foreach (var d in products)
            {
                string imageUrl = string.IsNullOrWhiteSpace(d.ImageUrl) ? "images/products/default.jpg" : d.ImageUrl;
                sb.Append("<div class='card'>")
                  .Append("<div class='img'><img src='").Append(WebUtility.HtmlEncode(imageUrl)).Append("' alt='").Append(WebUtility.HtmlEncode(d.Title)).Append("' onerror=\"this.onerror=null;this.src='images/products/default.jpg';\"></div>")
                  .Append("<div class='title'>").Append(WebUtility.HtmlEncode(d.Title)).Append("</div>")
                  .Append("<div class='sub'>").Append(WebUtility.HtmlEncode(d.Sub)).Append("</div>")
                  .Append("<div class='meta'><div><div class='price'>").Append(WebUtility.HtmlEncode(d.Price)).Append("</div></div>")
                  .Append("<div class='stock'><div class='sub'>Stock</div>").Append(WebUtility.HtmlEncode(d.Stock)).Append("</div></div>")
                  .Append("<div class='btns'><button class='ghost' tabindex='-1'>Details</button><button class='dark' tabindex='-1'>Apply</button></div>")
                  .Append("</div>");
            }

            return sb.ToString();
        }
    }
}
