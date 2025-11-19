// Revolution Slider Custom Implementation
// Giống Revolution Slider với layer animations

class RevolutionSlider {
    constructor(container, options = {}) {
        this.container = document.querySelector(container);
        this.slides = this.container.querySelectorAll('.rev-slide');
        this.currentSlide = 0;
        this.isAnimating = false;
        
        this.options = {
            autoplay: options.autoplay !== false,
            delay: options.delay || 6000,
            transitionSpeed: options.transitionSpeed || 1000,
            navigation: options.navigation !== false,
            pagination: options.pagination !== false,
            ...options
        };
        
        this.init();
    }
    
    init() {
        this.setupSlides();
        this.setupNavigation();
        this.setupPagination();
        
        if (this.options.autoplay) {
            this.startAutoplay();
        }
        
        // Show first slide
        this.showSlide(0);
    }
    
    setupSlides() {
        this.slides.forEach((slide, index) => {
            slide.style.opacity = '0';
            slide.style.position = 'absolute';
            slide.style.top = '0';
            slide.style.left = '0';
            slide.style.width = '100%';
            slide.style.height = '100%';
            
            if (index === 0) {
                slide.classList.add('active');
            }
        });
    }
    
    setupNavigation() {
        if (!this.options.navigation) return;
        
        const prevBtn = this.container.querySelector('.rev-nav-prev');
        const nextBtn = this.container.querySelector('.rev-nav-next');
        
        if (prevBtn) {
            prevBtn.addEventListener('click', () => this.prevSlide());
        }
        
        if (nextBtn) {
            nextBtn.addEventListener('click', () => this.nextSlide());
        }
    }
    
    setupPagination() {
        if (!this.options.pagination) return;
        
        const pagination = this.container.querySelector('.rev-pagination');
        if (!pagination) return;
        
        pagination.innerHTML = '';
        
        this.slides.forEach((_, index) => {
            const bullet = document.createElement('span');
            bullet.classList.add('rev-bullet');
            if (index === 0) bullet.classList.add('active');
            
            bullet.addEventListener('click', () => this.goToSlide(index));
            pagination.appendChild(bullet);
        });
    }
    
    showSlide(index) {
        if (this.isAnimating) return;
        this.isAnimating = true;
        
        const currentSlide = this.slides[this.currentSlide];
        const nextSlide = this.slides[index];
        
        // Hide current slide with animation
        this.animateOut(currentSlide);
        
        // Show next slide with animation
        setTimeout(() => {
            this.animateIn(nextSlide);
            this.currentSlide = index;
            this.updatePagination();
            
            setTimeout(() => {
                this.isAnimating = false;
            }, this.options.transitionSpeed);
        }, this.options.transitionSpeed / 2);
    }
    
    animateOut(slide) {
        slide.classList.remove('active');
        const layers = slide.querySelectorAll('[data-animation]');
        
        layers.forEach((layer, index) => {
            const delay = index * 100;
            setTimeout(() => {
                layer.style.animation = 'fadeOutUp 0.6s ease-out forwards';
            }, delay);
        });
        
        setTimeout(() => {
            slide.style.opacity = '0';
            slide.style.zIndex = '1';
        }, this.options.transitionSpeed / 2);
    }
    
    animateIn(slide) {
        slide.style.opacity = '1';
        slide.style.zIndex = '2';
        slide.classList.add('active');
        
        const layers = slide.querySelectorAll('[data-animation]');
        
        layers.forEach((layer, index) => {
            const animation = layer.getAttribute('data-animation') || 'fadeInUp';
            const delay = (index * 150) + 300;
            
            layer.style.opacity = '0';
            
            setTimeout(() => {
                layer.style.animation = `${animation} 0.8s ease-out forwards`;
                layer.style.opacity = '1';
            }, delay);
        });
    }
    
    nextSlide() {
        const next = (this.currentSlide + 1) % this.slides.length;
        this.showSlide(next);
        this.resetAutoplay();
    }
    
    prevSlide() {
        const prev = (this.currentSlide - 1 + this.slides.length) % this.slides.length;
        this.showSlide(prev);
        this.resetAutoplay();
    }
    
    goToSlide(index) {
        if (index !== this.currentSlide) {
            this.showSlide(index);
            this.resetAutoplay();
        }
    }
    
    updatePagination() {
        const bullets = this.container.querySelectorAll('.rev-bullet');
        bullets.forEach((bullet, index) => {
            bullet.classList.toggle('active', index === this.currentSlide);
        });
    }
    
    startAutoplay() {
        this.autoplayInterval = setInterval(() => {
            this.nextSlide();
        }, this.options.delay);
    }
    
    stopAutoplay() {
        if (this.autoplayInterval) {
            clearInterval(this.autoplayInterval);
        }
    }
    
    resetAutoplay() {
        if (this.options.autoplay) {
            this.stopAutoplay();
            this.startAutoplay();
        }
    }
}

// Animations CSS (will be injected)
const animationStyles = `
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(50px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    @keyframes fadeOutUp {
        from {
            opacity: 1;
            transform: translateY(0);
        }
        to {
            opacity: 0;
            transform: translateY(-50px);
        }
    }
    
    @keyframes fadeInLeft {
        from {
            opacity: 0;
            transform: translateX(-100px);
        }
        to {
            opacity: 1;
            transform: translateX(0);
        }
    }
    
    @keyframes fadeInRight {
        from {
            opacity: 0;
            transform: translateX(100px);
        }
        to {
            opacity: 1;
            transform: translateX(0);
        }
    }
    
    @keyframes zoomIn {
        from {
            opacity: 0;
            transform: scale(0.5);
        }
        to {
            opacity: 1;
            transform: scale(1);
        }
    }
    
    @keyframes slideInDown {
        from {
            opacity: 0;
            transform: translateY(-100px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
`;

// Inject animation styles
const styleSheet = document.createElement('style');
styleSheet.textContent = animationStyles;
document.head.appendChild(styleSheet);

// Export for use
window.RevolutionSlider = RevolutionSlider;
