// =============================================
//  ShopEZ — cart.js
//  Cart CRUD operations with LocalStorage
// =============================================

"use strict";

// ---- Get cart ----
function getCart() {
  return JSON.parse(localStorage.getItem("cart")) || [];
}

// ---- Save cart ----
function saveCart(cart) {
  localStorage.setItem("cart", JSON.stringify(cart));
  updateCartCount();
}

// ---- Add to cart ----
function addToCart(product) {
  const cart = getCart();
  const existing = cart.find((p) => p.id === product.id);
  if (existing) {
    showToast(`${product.name} is already in your cart!`, "info");
    return false;
  }
  cart.push(product);
  saveCart(cart);
  showToast(`${product.name} added to cart! 🛍️`, "success");
  return true;
}

// ---- Remove from cart ----
function removeFromCart(productId) {
  let cart = getCart();
  const item = cart.find((p) => p.id === productId);
  cart = cart.filter((p) => p.id !== productId);
  saveCart(cart);
  if (item) showToast(`${item.name} removed from cart.`, "info");
  return cart;
}

// ---- Calculate total ----
function calculateTotal(cart) {
  return cart.reduce((sum, item) => sum + item.price, 0);
}

// ---- Is in cart ----
function isInCart(productId) {
  return getCart().some((p) => p.id === productId);
}

// ---- Render cart page ----
function renderCartPage() {
  const cart = getCart();
  const $itemsWrap = $("#cart-items-wrap");
  const $emptyState = $("#cart-empty");
  const $summarySection = $("#cart-summary-section");

  if (cart.length === 0) {
    $itemsWrap.empty();
    $emptyState.show();
    $summarySection.hide();
    return;
  }

  $emptyState.hide();
  $summarySection.show();
  $itemsWrap.empty();

  cart.forEach((item, index) => {
    const discount = item.originalPrice
      ? `<span class="price-original">${formatPrice(item.originalPrice)}</span>`
      : "";
    const $card = $(`
      <div class="cart-item fade-in" style="animation-delay:${index * 0.08}s">
        <img src="${item.image}" alt="${item.name}" class="cart-item-img" onerror="this.src='https://via.placeholder.com/100x80/1a1a1a/f5a623?text=IMG'">
        <div>
          <p class="product-category">${item.category || "Product"}</p>
          <h3 class="cart-item-name">${item.name}</h3>
          <div class="cart-item-price">${formatPrice(item.price)} ${discount}</div>
        </div>
        <button class="btn-remove" data-id="${item.id}">✕ Remove</button>
      </div>
    `);
    $itemsWrap.append($card);
  });

  updateCartSummary(cart);

  // Remove button handler
  $(document).off("click", ".btn-remove").on("click", ".btn-remove", function () {
    const id = parseInt($(this).data("id"));
    const updatedCart = removeFromCart(id);
    renderCartPage();
  });
}

// ---- Update cart summary ----
function updateCartSummary(cart) {
  const subtotal = calculateTotal(cart);
  const shipping = subtotal > 50000 ? 0 : 99;
  const total = subtotal + shipping;

  $("#summary-subtotal").text(formatPrice(subtotal));
  $("#summary-shipping").text(shipping === 0 ? "FREE" : formatPrice(shipping));
  $("#summary-total").text(formatPrice(total));
  $("#summary-count").text(cart.length + " item" + (cart.length > 1 ? "s" : ""));
}

// ---- Init cart page ----
$(document).ready(function () {
  if ($("#cart-items-wrap").length) {
    renderCartPage();
  }
});
