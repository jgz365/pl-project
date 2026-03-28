namespace customer_kiosk
{
    internal static class CatalogHtmlProvider
    {
        public static string GetHtml()
        {
            return """
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <style>
    body{font-family:'Inter','Poppins','Montserrat','Roboto','Segoe UI',sans-serif;margin:0;background:#f3f4f6;color:#0f172a}
    .wrap{padding:20px;display:grid;grid-template-columns:repeat(auto-fill,minmax(320px,1fr));gap:18px}
    .card{background:#fff;border-radius:14px;padding:14px;box-shadow:0 8px 20px rgba(0,0,0,.08)}
    .img{height:180px;border-radius:10px;background:linear-gradient(135deg,#dbeafe,#cbd5e1);display:flex;align-items:center;justify-content:center;color:#64748b;font-weight:600}
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
    .hero{margin:10px 18px;height:320px;border-radius:10px;background:linear-gradient(135deg,#dbeafe,#cbd5e1)}
    .content{padding:0 18px 18px}
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
  <div class="wrap" id="cards"></div>

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
      <div class="content">
        <div class="meta">
          <div><div class="sub">Price</div><div class="price" id="mPrice">₱145,000.00</div></div>
          <div class="stock"><div class="sub">Stock</div><div id="mStock">1 units</div></div>
        </div>
        <div class="divider"></div>
        <div class="section-title">Specifications</div>
        <div class="spec-grid">
          <div class="spec-item"><div class="spec-icon">◔</div><div><div class="spec-label">Engine</div><div class="spec-value" id="mEngine">156cc</div></div></div>
          <div class="spec-item"><div class="spec-icon">⚡</div><div><div class="spec-label">Max Power</div><div class="spec-value" id="mPower">15.8 hp @ 8,500 rpm</div></div></div>
          <div class="spec-item"><div class="spec-icon">↔</div><div><div class="spec-label">Max Torque</div><div class="spec-value" id="mTorque">14.7 Nm @ 6,500 rpm</div></div></div>
          <div class="spec-item"><div class="spec-icon">⛽</div><div><div class="spec-label">Fuel Capacity</div><div class="spec-value" id="mFuel">8 liters</div></div></div>
          <div class="spec-item"><div class="spec-icon">⚙</div><div><div class="spec-label">Transmission</div><div class="spec-value" id="mTrans">CVT</div></div></div>
          <div class="spec-item"><div class="spec-icon">↕</div><div><div class="spec-label">Seat Height</div><div class="spec-value" id="mSeat">795 mm</div></div></div>
          <div class="spec-item"><div class="spec-icon">◍</div><div><div class="spec-label">Weight</div><div class="spec-value" id="mWeight">133 kg</div></div></div>
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
    const data = [
      { title:'Honda ADV 160', sub:'2024 • Sport', price:'₱145,000.00', stock:'1 units', specs:'Engine 156cc • Max Power 15.8 hp • Fuel 8L', desc:'Repossessed unit in good condition.' },
      { title:'Suzuki Raider R150', sub:'2025 • Underbone', price:'₱178,000.00', stock:'5 units', specs:'Engine 150cc • Fuel 8L', desc:'{Description Placeholder}' },
      { title:'Yamaha NMAX 155', sub:'2024 • Scooter', price:'₱151,000.00', stock:'3 units', specs:'Engine 155cc • Fuel 7.1L', desc:'{Description Placeholder}' },
      { title:'Kawasaki Ninja 400', sub:'2023 • Sport', price:'₱331,000.00', stock:'2 units', specs:'Engine 399cc • Fuel 14L', desc:'{Description Placeholder}' },
      { title:'KTM RC 200', sub:'2024 • Sport', price:'₱165,000.00', stock:'4 units', specs:'Engine 200cc • Fuel 13.7L', desc:'{Description Placeholder}' },
      { title:'CFMOTO 300NK', sub:'2025 • Standard', price:'₱172,000.00', stock:'6 units', specs:'Engine 292cc • Fuel 12.5L', desc:'{Description Placeholder}' },
      { title:'Honda Click 160', sub:'2025 • Scooter', price:'₱122,900.00', stock:'8 units', specs:'Engine 157cc • Fuel 5.5L', desc:'{Description Placeholder}' },
      { title:'Yamaha MT-15', sub:'2024 • Naked', price:'₱164,000.00', stock:'3 units', specs:'Engine 155cc • Fuel 10L', desc:'{Description Placeholder}' },
      { title:'Kawasaki Dominar 400', sub:'2023 • Touring', price:'₱229,000.00', stock:'2 units', specs:'Engine 373cc • Fuel 13L', desc:'{Description Placeholder}' },
      { title:'Suzuki Burgman Street', sub:'2024 • Scooter', price:'₱92,400.00', stock:'7 units', specs:'Engine 125cc • Fuel 5.5L', desc:'{Description Placeholder}' },
      { title:'BMW G 310 R', sub:'2025 • Standard', price:'₱295,000.00', stock:'1 units', specs:'Engine 313cc • Fuel 11L', desc:'{Description Placeholder}' },
      { title:'Ducati Monster', sub:'2024 • Naked', price:'₱875,000.00', stock:'1 units', specs:'Engine 937cc • Fuel 14L', desc:'{Description Placeholder}' }
    ];
    const cards = document.getElementById('cards');
    let current = data[0];
    data.forEach(d=>{
      const c=document.createElement('div'); c.className='card';
      c.innerHTML=`<div class='img'>Sample Image</div><div class='title'>${d.title}</div><div class='sub'>${d.sub}</div><div class='meta'><div><div class='price'>${d.price}</div></div><div class='stock'><div class='sub'>Stock</div>${d.stock}</div></div><div class='btns'><button class='ghost' tabindex='-1'>Details</button><button class='dark' tabindex='-1'>Apply</button></div>`;
      c.querySelector('.ghost').addEventListener('click',()=>showDetails(d));
      c.querySelector('.dark').addEventListener('click',()=>openApplication());
      cards.appendChild(c);
    });
    function showDetails(d){
      current=d;
      document.getElementById('mTitle').textContent=d.title;
      document.getElementById('mSub').textContent=d.sub;
      document.getElementById('mPrice').textContent=d.price;
      document.getElementById('mStock').textContent=d.stock;
      document.getElementById('mEngine').textContent=(d.engine||'156cc');
      document.getElementById('mPower').textContent=(d.power||'15.8 hp @ 8,500 rpm');
      document.getElementById('mTorque').textContent=(d.torque||'14.7 Nm @ 6,500 rpm');
      document.getElementById('mFuel').textContent=(d.fuel||'8 liters');
      document.getElementById('mTrans').textContent=(d.trans||'CVT');
      document.getElementById('mSeat').textContent=(d.seat||'795 mm');
      document.getElementById('mWeight').textContent=(d.weight||'133 kg');
      document.getElementById('mDesc').textContent=d.desc;
      document.getElementById('overlay').style.display='flex';
    }
    function openApplication(){
      window.chrome?.webview?.postMessage('open_redirect_application');
    }
    function hideModal(){ document.getElementById('overlay').style.display='none'; }
    function applyNow(){ openApplication(); }
  </script>
</body>
</html>
""";
        }
    }
}
