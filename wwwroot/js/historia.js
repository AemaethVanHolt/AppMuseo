// Animación para la línea de tiempo
document.addEventListener('DOMContentLoaded', function() {
    const eventos = document.querySelectorAll('.evento');
    
    // Función para verificar si un elemento está en el viewport
    function isInViewport(element) {
        const rect = element.getBoundingClientRect();
        return (
            rect.top <= (window.innerHeight || document.documentElement.clientHeight) * 0.8 &&
            rect.bottom >= 0
        );
    }
    
    // Función para manejar el scroll
    function handleScroll() {
        eventos.forEach(evento => {
            if (isInViewport(evento) && !evento.classList.contains('visible')) {
                evento.classList.add('visible');
            }
        });
    }
    
    // Mostrar eventos visibles al cargar la página
    handleScroll();
    
    // Escuchar el evento de scroll
    window.addEventListener('scroll', handleScroll);
});
