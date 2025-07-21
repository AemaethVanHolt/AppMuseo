// Función para actualizar el precio de la entrada reducida
function actualizarPrecioReducido() {
    const tipoDescuento = document.getElementById('tipoDescuento');
    const precioReducidoInput = document.getElementById('precioReducidoInput');
    const tipoDescuentoInput = document.getElementById('tipoDescuentoInput');
    const precioMostrado = document.getElementById('precioReducidoMostrado');
    
    if (tipoDescuento) {
        const opcionSeleccionada = tipoDescuento.options[tipoDescuento.selectedIndex];
        const nuevoPrecio = opcionSeleccionada.dataset.precio;
        const textoPrecio = nuevoPrecio === '0.00' ? 'Gratis' : `${parseFloat(nuevoPrecio).toFixed(2).replace('.', ',')} €`;
        
        // Actualizar el precio mostrado
        if (precioMostrado) {
            precioMostrado.textContent = textoPrecio;
        }
        
        // Actualizar los campos ocultos del formulario
        if (precioReducidoInput) {
            precioReducidoInput.value = nuevoPrecio;
            precioReducidoInput.dataset.precio = nuevoPrecio;
        }
        
        if (tipoDescuentoInput) {
            tipoDescuentoInput.value = opcionSeleccionada.value;
        }
    }
}

// Inicialización al cargar la página
document.addEventListener('DOMContentLoaded', function() {
    // Inicializar el precio reducido
    actualizarPrecioReducido();
    
    // Configurar el evento change
    const tipoDescuento = document.getElementById('tipoDescuento');
    if (tipoDescuento) {
        tipoDescuento.addEventListener('change', actualizarPrecioReducido);
    }
    
    // Validación de formularios
    const forms = document.querySelectorAll('form[id^="form"]');
    const mensajeError = document.getElementById('mensajeError');
    
    forms.forEach(form => {
        form.addEventListener('submit', function(e) {
            const cantidad = this.querySelector('input[type="number"]').value;
            
            if (!cantidad || cantidad < 1) {
                e.preventDefault();
                mensajeError.classList.remove('d-none');
                window.scrollTo({ top: 0, behavior: 'smooth' });
            } else {
                mensajeError.classList.add('d-none');
            }
        });
    });
    
    // Actualizar precio cuando cambia la cantidad
    const inputsCantidad = document.querySelectorAll('input[type="number"]');
    inputsCantidad.forEach(input => {
        input.addEventListener('change', function() {
            if (this.value < 1) {
                this.value = 1;
            }
        });
    });
});
