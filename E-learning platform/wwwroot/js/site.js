// ============================================
//  E-LEARN.PRO — site.js
// ============================================

document.addEventListener('DOMContentLoaded', () => {

  // ── Navbar scroll effect ──────────────────
  const nav = document.querySelector('.glass-nav');
  if (nav) {
    window.addEventListener('scroll', () => {
      nav.classList.toggle('scrolled', window.scrollY > 40);
    });
  }

  // ── Mobile menu toggle ────────────────────
  const toggler = document.getElementById('navToggler');
  const mobileMenu = document.getElementById('mobileMenu');
  if (toggler && mobileMenu) {
    toggler.addEventListener('click', () => {
      mobileMenu.classList.toggle('open');
      const icon = toggler.querySelector('i');
      if (icon) {
        icon.className = mobileMenu.classList.contains('open')
          ? 'bi bi-x-lg' : 'bi bi-list';
      }
    });
  }

  // ── Scroll reveal ─────────────────────────
  const reveals = document.querySelectorAll('.reveal');
  if (reveals.length) {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.classList.add('visible');
          observer.unobserve(e.target);
        }
      });
    }, { threshold: 0.12 });
    reveals.forEach(el => observer.observe(el));
  }

  // ── Animated counters ─────────────────────
  function animateCounter(el) {
    const target = parseFloat(el.dataset.target);
    const suffix = el.dataset.suffix || '';
    const decimals = el.dataset.decimals || 0;
    const duration = 2000;
    const start = performance.now();
    const step = (now) => {
      const elapsed = now - start;
      const progress = Math.min(elapsed / duration, 1);
      const eased = 1 - Math.pow(1 - progress, 3);
      const value = (target * eased).toFixed(decimals);
      el.textContent = value + suffix;
      if (progress < 1) requestAnimationFrame(step);
    };
    requestAnimationFrame(step);
  }

  const counterEls = document.querySelectorAll('[data-counter]');
  if (counterEls.length) {
    const counterObserver = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          animateCounter(e.target);
          counterObserver.unobserve(e.target);
        }
      });
    }, { threshold: 0.5 });
    counterEls.forEach(el => counterObserver.observe(el));
  }

  // ── Progress bars animate on view ─────────
  const progressFills = document.querySelectorAll('.progress-fill[data-width]');
  if (progressFills.length) {
    const progObserver = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.style.width = e.target.dataset.width + '%';
          progObserver.unobserve(e.target);
        }
      });
    }, { threshold: 0.5 });
    progressFills.forEach(el => {
      el.style.width = '0%';
      progObserver.observe(el);
    });
  }

  // ── Toast notification helper ─────────────
  window.showToast = function(msg, type = 'success') {
    const toast = document.createElement('div');
    toast.style.cssText = `
      position:fixed; bottom:2rem; right:2rem; z-index:9999;
      background:${type === 'success' ? 'rgba(0,201,125,0.15)' : 'rgba(0,98,255,0.15)'};
      border:1px solid ${type === 'success' ? 'rgba(0,201,125,0.4)' : 'rgba(0,98,255,0.4)'};
      color:${type === 'success' ? '#00c97d' : '#00d2ff'};
      padding:1rem 1.5rem; border-radius:14px;
      font-size:0.9rem; font-weight:600;
      backdrop-filter:blur(20px);
      animation: slideInUp 0.4s ease;
      box-shadow: 0 10px 40px rgba(0,0,0,0.4);
    `;
    toast.textContent = msg;
    const style = document.createElement('style');
    style.textContent = '@keyframes slideInUp{from{opacity:0;transform:translateY(20px)}to{opacity:1;transform:translateY(0)}}';
    document.head.appendChild(style);
    document.body.appendChild(toast);
    setTimeout(() => { toast.style.opacity = '0'; toast.style.transition = 'opacity 0.3s'; setTimeout(() => toast.remove(), 300); }, 3500);
  };

  // ── Enroll button micro-feedback ──────────
  document.querySelectorAll('.btn-premium').forEach(btn => {
    btn.addEventListener('click', function(e) {
      if (this.dataset.feedback) {
        window.showToast(this.dataset.feedback);
      }
    });
  });

});
