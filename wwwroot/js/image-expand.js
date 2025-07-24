// Función para expandir la imagen
function expandImage(button) {
    const container = button.closest('.image-container');
    const img = container.querySelector('img');
    const lightbox = document.getElementById('lightbox');
    const lightboxImg = document.getElementById('lightbox-img');
    
    if (lightbox && lightboxImg) {
        lightboxImg.src = img.src;
        lightboxImg.alt = img.alt;
        lightbox.classList.add('active');
        document.body.style.overflow = 'hidden'; // Evitar scroll del body cuando el lightbox está abierto
    }
}

// Cerrar lightbox al hacer clic en el botón de cerrar
document.addEventListener('DOMContentLoaded', function() {
    const closeBtn = document.querySelector('.close-btn');
    const lightbox = document.getElementById('lightbox');
    
    if (closeBtn && lightbox) {
        closeBtn.addEventListener('click', function() {
            lightbox.classList.remove('active');
            document.body.style.overflow = 'auto'; // Restaurar scroll del body
        });
    }
    
    // Cerrar lightbox al hacer clic fuera de la imagen
    if (lightbox) {
        lightbox.addEventListener('click', function(e) {
            if (e.target === lightbox) {
                lightbox.classList.remove('active');
                document.body.style.overflow = 'auto'; // Restaurar scroll del body
            }
        });
    }
    
    // Cerrar con la tecla ESC
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape' && lightbox && lightbox.classList.contains('active')) {
            lightbox.classList.remove('active');
            document.body.style.overflow = 'auto'; // Restaurar scroll del body
        }
    });
});
