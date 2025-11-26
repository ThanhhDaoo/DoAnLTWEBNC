// ========== REMOS ADMIN JS ==========

// Toggle Sidebar
function toggleSidebar() {
    const sidebar = document.querySelector('.section-menu-left');
    const overlay = document.querySelector('.overlay');

    if (sidebar) sidebar.classList.toggle('active');
    if (overlay) overlay.classList.toggle('active');
    document.body.classList.toggle('sidebar-open');
}

// Initialize
document.addEventListener('DOMContentLoaded', function () {
    // Close sidebar on overlay click
    const overlay = document.querySelector('.overlay');
    if (overlay) {
        overlay.addEventListener('click', toggleSidebar);
    }

    // Close sidebar on resize
    window.addEventListener('resize', function () {
        if (window.innerWidth > 991) {
            const sidebar = document.querySelector('.section-menu-left');
            const overlay = document.querySelector('.overlay');
            if (sidebar) sidebar.classList.remove('active');
            if (overlay) overlay.classList.remove('active');
            document.body.classList.remove('sidebar-open');
        }
    });

    // Initialize tooltips
    const tooltips = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltips.forEach(el => new bootstrap.Tooltip(el));

    // Animate numbers
    animateNumbers();
});

// Animate stat numbers
function animateNumbers() {
    const elements = document.querySelectorAll('.stat-value, .sales-amount, .revenue-value');

    elements.forEach(el => {
        const text = el.textContent;
        const match = text.match(/[\d,]+/);

        if (match) {
            const target = parseInt(match[0].replace(/,/g, ''));
            const suffix = text.replace(match[0], '');
            const duration = 800;
            const step = target / (duration / 16);
            let current = 0;

            const timer = setInterval(() => {
                current += step;
                if (current >= target) {
                    current = target;
                    clearInterval(timer);
                }
                el.innerHTML = formatNumber(Math.floor(current)) + suffix;
            }, 16);
        }
    });
}

// Format number
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

// Format currency
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN').format(amount) + '₫';
}

// Show toast
function showToast(message, type = 'info') {
    const container = document.getElementById('toast-container') || createToastContainer();

    const icons = {
        success: 'fa-check-circle',
        error: 'fa-exclamation-circle',
        warning: 'fa-exclamation-triangle',
        info: 'fa-info-circle'
    };

    const colors = {
        success: '#22C55E',
        error: '#FF5200',
        warning: '#FF9F43',
        info: '#2377FC'
    };

    const toast = document.createElement('div');
    toast.className = 'toast-item';
    toast.style.cssText = `
        display: flex;
        align-items: center;
        gap: 12px;
        padding: 14px 18px;
        background: white;
        border-radius: 8px;
        box-shadow: 0 4px 20px rgba(0,0,0,0.15);
        transform: translateX(120%);
        transition: transform 0.3s ease;
        margin-bottom: 10px;
    `;

    toast.innerHTML = `
        <i class="fas ${icons[type]}" style="color: ${colors[type]}; font-size: 18px;"></i>
        <span style="font-size: 14px; color: #111;">${message}</span>
        <button onclick="this.parentElement.remove()" style="background: none; border: none; color: #95989D; cursor: pointer; margin-left: auto;">
            <i class="fas fa-times"></i>
        </button>
    `;

    container.appendChild(toast);

    setTimeout(() => toast.style.transform = 'translateX(0)', 10);

    setTimeout(() => {
        toast.style.transform = 'translateX(120%)';
        setTimeout(() => toast.remove(), 300);
    }, 4000);
}

function createToastContainer() {
    const container = document.createElement('div');
    container.id = 'toast-container';
    container.style.cssText = 'position: fixed; top: 20px; right: 20px; z-index: 9999;';
    document.body.appendChild(container);
    return container;
}

// Confirm action
function confirmAction(message, callback) {
    if (confirm(message)) callback();
}

// Search filter
function initSearch(inputSelector, tableSelector) {
    const input = document.querySelector(inputSelector);
    const table = document.querySelector(tableSelector);

    if (!input || !table) return;

    input.addEventListener('keyup', function () {
        const filter = this.value.toLowerCase();
        const rows = table.querySelectorAll('tbody tr');

        rows.forEach(row => {
            const text = row.textContent.toLowerCase();
            row.style.display = text.includes(filter) ? '' : 'none';
        });
    });
}

// Loading state
function setLoading(btn, loading) {
    if (loading) {
        btn.disabled = true;
        btn.dataset.text = btn.innerHTML;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Đang xử lý...';
    } else {
        btn.disabled = false;
        btn.innerHTML = btn.dataset.text || btn.innerHTML;
    }
}