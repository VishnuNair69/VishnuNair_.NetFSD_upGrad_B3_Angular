// =============================================
//  ShopEZ — common.js
//  Shared utilities: nav, cart count, toast
// =============================================

"use strict";

// ---- Cart Count ----
function updateCartCount() {
  const cart = JSON.parse(localStorage.getItem("cart")) || [];
  $(".cart-count").text(cart.length);
}

// ---- Toast Notification ----
function showToast(message, type = "success", duration = 3000) {
  const icons = { success: "✓", error: "✕", info: "ℹ" };
  const $toast = $(`
    <div class="toast ${type}">
      <span class="toast-icon">${icons[type] || icons.info}</span>
      <span>${message}</span>
    </div>
  `);

  if (!$(".toast-container").length) {
    $("body").append('<div class="toast-container"></div>');
  }
  $(".toast-container").append($toast);

  setTimeout(() => {
    $toast.addClass("removing");
    setTimeout(() => $toast.remove(), 300);
  }, duration);
}

// ---- Stars renderer ----
function renderStars(rating) {
  let stars = "";
  for (let i = 1; i <= 5; i++) {
    if (i <= Math.floor(rating)) stars += "★";
    else if (i - 0.5 <= rating) stars += "½";
    else stars += "☆";
  }
  return stars;
}

// ---- Format currency (INR) ----
function formatPrice(price) {
  return "₹" + price.toLocaleString("en-IN");
}

// ---- Discount percent ----
function discountPercent(original, current) {
  return Math.round(((original - current) / original) * 100);
}

// ---- Mark active nav link ----
function markActiveNav() {
  const page = window.location.pathname.split("/").pop() || "index.html";
  $(".nav-links a").each(function () {
    const href = $(this).attr("href");
    if (href === page) $(this).addClass("active");
  });
}

// ---- Page fade in ----
function initPageFade() {
  $("body").css({ opacity: 0 });
  $(document).ready(function () {
    $("body").animate({ opacity: 1 }, 400);
  });
}

// ---- On DOM ready ----
$(document).ready(function () {
  updateCartCount();
  markActiveNav();
  initPageFade();
});
