// =============================================
//  ShopEZ — products.js
//  Load and display products from JSON
// =============================================

"use strict";

let allProducts = [];

// ---- Load products from JSON ----
function loadProducts(callback) {
  $.getJSON("data/products.json", function (products) {
    allProducts = products;
    if (callback) callback(products);
  }).fail(function () {
    showToast("Failed to load products. Using demo data.", "error");
    // Fallback inline demo products
    allProducts = getDemoProducts();
    if (callback) callback(allProducts);
  });
}

// ---- Render product cards ----
function renderProductCards(products, containerId) {
  const $container = $(containerId || "#products-grid");
  $container.empty();

  if (!products || products.length === 0) {
    $container.append(`
      <div class="no-results">
        <div class="no-results-icon">🔍</div>
        <p>No products found.</p>
      </div>
    `);
    return;
  }

  products.forEach((product, index) => {
    const badgeHtml = product.badge
      ? `<span class="product-badge">${product.badge}</span>`
      : "";
    const discount = product.originalPrice
      ? discountPercent(product.originalPrice, product.price)
      : 0;
    const discountHtml = discount > 0
      ? `<span class="price-discount">−${discount}%</span>`
      : "";
    const origHtml = product.originalPrice
      ? `<span class="price-original">${formatPrice(product.originalPrice)}</span>`
      : "";

    const inCart = isInCart(product.id);

    const $card = $(`
      <div class="product-card fade-in" style="animation-delay:${index * 0.06}s" data-id="${product.id}">
        <div class="product-img-wrap">
          ${badgeHtml}
          <img src="${product.image}" alt="${product.name}" loading="lazy"
               onerror="this.src='https://via.placeholder.com/400x300/1a1a1a/f5a623?text=${encodeURIComponent(product.name)}'">
        </div>
        <div class="product-body">
          <p class="product-category">${product.category || "Product"}</p>
          <h3 class="product-name">${product.name}</h3>
          <div class="product-rating">
            <span class="stars">${renderStars(product.rating || 4)}</span>
            <span class="rating-count">(${product.reviews || 0})</span>
          </div>
          <div class="product-price-row">
            <span class="price-current">${formatPrice(product.price)}</span>
            ${origHtml}
            ${discountHtml}
          </div>
          <div class="product-actions">
            <button class="btn-cart ${inCart ? "added" : ""}" data-id="${product.id}">
              ${inCart ? "✓ Added" : "🛒 Add to Cart"}
            </button>
            <a href="product-details.html?id=${product.id}" class="btn-details" title="View Details">👁</a>
          </div>
        </div>
      </div>
    `);
    $container.append($card);
  });

  // Add to cart button handler
  $(document).off("click", ".btn-cart").on("click", ".btn-cart", function (e) {
    e.stopPropagation();
    const id = parseInt($(this).data("id"));
    const product = allProducts.find((p) => p.id === id);
    if (!product) return;

    const added = addToCart(product);
    if (added) {
      $(this).addClass("added").text("✓ Added");
    }
  });
}

// ---- Get single product by id ----
function getProductById(id) {
  return allProducts.find((p) => p.id === id) || null;
}

// ---- Filter products by category ----
function filterByCategory(category) {
  if (category === "All") return allProducts;
  return allProducts.filter((p) => p.category === category);
}

// ---- Get categories ----
function getCategories() {
  const cats = [...new Set(allProducts.map((p) => p.category))];
  return ["All", ...cats];
}

// ---- Demo fallback data ----
function getDemoProducts() {
  return [
    { id: 1, name: "ProBook Laptop", category: "Electronics", description: "High-performance laptop.", price: 60000, originalPrice: 72000, rating: 4.5, reviews: 128, image: "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=600&q=80", badge: "Best Seller" },
    { id: 2, name: "Nova Smartphone", category: "Electronics", description: "Latest flagship smartphone.", price: 25000, originalPrice: 30000, rating: 4.3, reviews: 245, image: "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=600&q=80", badge: "New" },
    { id: 3, name: "SoundWave Headphones", category: "Audio", description: "Premium wireless headphones.", price: 8500, originalPrice: 12000, rating: 4.7, reviews: 89, image: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=600&q=80", badge: "Sale" },
    { id: 4, name: "SmartWatch Pro", category: "Wearables", description: "Advanced smartwatch.", price: 15000, originalPrice: 18000, rating: 4.2, reviews: 67, image: "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=600&q=80", badge: "" }
  ];
}
