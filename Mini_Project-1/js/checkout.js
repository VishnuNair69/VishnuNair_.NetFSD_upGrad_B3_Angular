// =============================================
//  ShopEZ — checkout.js
//  Checkout form validation and order simulation
// =============================================

"use strict";

// ---- Render checkout summary ----
function renderCheckoutSummary() {
  const cart = getCart();
  const $list = $("#checkout-order-list");
  $list.empty();

  if (cart.length === 0) {
    window.location.href = "cart.html";
    return;
  }

  cart.forEach((item) => {
    $list.append(`
      <div class="summary-row">
        <span class="label">${item.name}</span>
        <span class="value">${formatPrice(item.price)}</span>
      </div>
    `);
  });

  const subtotal = calculateTotal(cart);
  const shipping = subtotal > 50000 ? 0 : 99;
  const total = subtotal + shipping;

  $("#co-subtotal").text(formatPrice(subtotal));
  $("#co-shipping").text(shipping === 0 ? "FREE" : formatPrice(shipping));
  $("#co-total").text(formatPrice(total));
}

// ---- Validate single field ----
function validateField($input) {
  const value = $input.val().trim();
  const type = $input.data("validate");
  let valid = true;
  let msg = "";

  if (type === "required" && value === "") {
    valid = false; msg = "This field is required.";
  } else if (type === "email") {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!re.test(value)) { valid = false; msg = "Enter a valid email address."; }
  } else if (type === "pin") {
    if (!/^\d{6}$/.test(value)) { valid = false; msg = "Enter a valid 6-digit PIN code."; }
  } else if (type === "phone") {
    if (!/^\d{10}$/.test(value)) { valid = false; msg = "Enter a valid 10-digit phone number."; }
  }

  if (!valid) {
    $input.addClass("error");
    $input.siblings(".form-error-msg").text(msg).show();
  } else {
    $input.removeClass("error");
    $input.siblings(".form-error-msg").hide();
  }
  return valid;
}

// ---- Validate full form ----
function validateForm() {
  let allValid = true;
  $("[data-validate]").each(function () {
    if (!validateField($(this))) allValid = false;
  });
  return allValid;
}

// ---- Generate order ID ----
function generateOrderId() {
  return "SEZ-" + Date.now().toString(36).toUpperCase() + "-" + Math.random().toString(36).substr(2, 4).toUpperCase();
}

// ---- Place order ----
function placeOrder(formData) {
  const cart = getCart();
  const orderId = generateOrderId();
  const order = {
    id: orderId,
    items: cart,
    total: calculateTotal(cart),
    customer: formData,
    date: new Date().toISOString(),
    status: "Confirmed"
  };

  // Save order history (optional feature)
  const orders = JSON.parse(localStorage.getItem("orders")) || [];
  orders.push(order);
  localStorage.setItem("orders", JSON.stringify(orders));

  // Clear cart
  localStorage.removeItem("cart");
  updateCartCount();

  // Redirect to success
  sessionStorage.setItem("lastOrder", JSON.stringify(order));
  window.location.href = "checkout.html?success=1&oid=" + orderId;
}

// ---- Render success state ----
function renderSuccessState() {
  const params = new URLSearchParams(window.location.search);
  const orderId = params.get("oid") || "SEZ-UNKNOWN";

  $("#checkout-form-section").hide();
  $("#checkout-success-section").show();
  $("#success-order-id").text(orderId);
}

// ---- Init checkout page ----
$(document).ready(function () {
  if (!$("#checkout-form").length) return;

  const params = new URLSearchParams(window.location.search);

  if (params.get("success") === "1") {
    renderSuccessState();
    return;
  }

  renderCheckoutSummary();

  // Live validation on blur
  $(document).on("blur", "[data-validate]", function () {
    validateField($(this));
  });

  // Clear error on focus
  $(document).on("focus", "[data-validate]", function () {
    $(this).removeClass("error");
    $(this).siblings(".form-error-msg").hide();
  });

  // Form submit
  $("#checkout-form").on("submit", function (e) {
    e.preventDefault();

    if (!validateForm()) {
      showToast("Please fill all required fields correctly.", "error");
      return;
    }

    const $btn = $("#place-order-btn");
    $btn.prop("disabled", true).text("Processing...");

    const formData = {
      name: $("#field-name").val().trim(),
      email: $("#field-email").val().trim(),
      phone: $("#field-phone").val().trim(),
      address: $("#field-address").val().trim(),
      city: $("#field-city").val().trim(),
      state: $("#field-state").val().trim(),
      pin: $("#field-pin").val().trim()
    };

    // Simulate processing delay
    setTimeout(() => {
      placeOrder(formData);
    }, 1500);
  });
});
