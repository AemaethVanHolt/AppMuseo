// Precios de los extras
const preciosExtras = {
    'audioguia': 2.50,
    'visita': 4.00,
    'foto': 1.50
};

// Función para formatear el precio
function formatearPrecio(precio) {
    return precio.toFixed(2).replace('.', ',') + ' €';
}

// Función para actualizar el precio total
function actualizarPrecioTotal(form) {
    if (!form) return;
    
    // Obtener el precio base
    const precioBaseInput = form.querySelector('input[name="precio"]');
    if (!precioBaseInput) return;
    
    // Obtener el precio base del data-precio o del value
    let precioBase = 0;
    if (precioBaseInput.dataset.precio) {
        precioBase = parseFloat(precioBaseInput.dataset.precio);
    } else if (precioBaseInput.value) {
        precioBase = parseFloat(precioBaseInput.value);
    }
    
    if (isNaN(precioBase)) precioBase = 0;
    
    let totalExtras = 0;
    
    // Sumar los precios de los extras seleccionados
    const checkboxes = form.querySelectorAll('input[type="checkbox"][name="extras"]:checked');
    checkboxes.forEach(checkbox => {
        const valorExtra = checkbox.value;
        if (preciosExtras[valorExtra] !== undefined) {
            totalExtras += parseFloat(preciosExtras[valorExtra]);
        }
    });
    
    // Calcular el total
    const total = precioBase + totalExtras;
    
    console.log('Precio base:', precioBase, 'Extras:', totalExtras, 'Total:', total);
    
    // Actualizar el precio mostrado
    const precioElement = form.querySelector('.precio-total');
    if (precioElement) {
        precioElement.textContent = formatearPrecio(total);
    }
    
    // Actualizar el campo oculto con el precio total
    let precioTotalInput = form.querySelector('input[name="precioTotal"]');
    if (!precioTotalInput) {
        precioTotalInput = document.createElement('input');
        precioTotalInput.type = 'hidden';
        precioTotalInput.name = 'precioTotal';
        form.appendChild(precioTotalInput);
    }
    precioTotalInput.value = total.toFixed(2);
    
    // Forzar actualización del formulario
    const event = new Event('change');
    precioTotalInput.dispatchEvent(event);
}

// Función para actualizar el precio de la entrada reducida
function actualizarPrecioReducido() {
    const tipoDescuento = document.getElementById('tipoDescuento');
    const precioReducidoInput = document.getElementById('precioReducidoInput');
    const tipoDescuentoInput = document.getElementById('tipoDescuentoInput');
    const precioMostrado = document.getElementById('precioReducidoMostrado');
    
    if (tipoDescuento) {
        const opcionSeleccionada = tipoDescuento.options[tipoDescuento.selectedIndex];
        const nuevoPrecio = opcionSeleccionada.dataset.precio;
        const textoPrecio = nuevoPrecio === '0.00' ? 'Gratis' : formatearPrecio(parseFloat(nuevoPrecio));
        
        // Actualizar el precio mostrado
        if (precioMostrado) {
            precioMostrado.textContent = textoPrecio;
        }
        
        // Actualizar los campos ocultos del formulario
        if (precioReducidoInput) {
            precioReducidoInput.value = nuevoPrecio;
            precioReducidoInput.dataset.precio = nuevoPrecio;
            
            // Actualizar el precio total
            const form = precioReducidoInput.closest('form');
            if (form) {
                actualizarPrecioTotal(form);
            }
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
    
    // Configurar el evento change para el selector de descuento
    const tipoDescuento = document.getElementById('tipoDescuento');
    if (tipoDescuento) {
        tipoDescuento.addEventListener('change', actualizarPrecioReducido);
    }
    
    // Configurar eventos para los checkboxes de extras
    document.querySelectorAll('input[type="checkbox"][name="extras"]').forEach(checkbox => {
        // Asegurarse de que el evento se dispara tanto para clics como para cambios
        ['change', 'click'].forEach(eventType => {
            checkbox.addEventListener(eventType, function() {
                console.log('Checkbox cambiado:', this.checked, this.value);
                const form = this.closest('form');
                if (form) {
                    actualizarPrecioTotal(form);
                }
            });
        });
    });
    
    // Inicializar precios totales para todos los formularios
    document.querySelectorAll('form[id^="form"]').forEach(form => {
        // Agregar elemento para mostrar el precio total si no existe
        const precioBaseElement = form.querySelector('h3 strong');
        if (precioBaseElement) {
            const precioTotalElement = document.createElement('div');
            precioTotalElement.className = 'precio-total-container mt-2';
            precioTotalElement.innerHTML = `<strong>Total: </strong><span class="precio-total">${precioBaseElement.textContent}</span>`;
            precioBaseElement.parentNode.insertBefore(precioTotalElement, precioBaseElement.nextSibling);
            
            // Actualizar el precio total inicial
            actualizarPrecioTotal(form);
        }
    });
    
    // Validación de formularios
    const forms = document.querySelectorAll('form[id^="form"]');
    const mensajeError = document.getElementById('mensajeError');
    
    forms.forEach(form => {
        form.addEventListener('submit', function(e) {
            const cantidad = this.querySelector('input[type="number"]')?.value || 1;
            
            if (cantidad < 1) {
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
            const form = this.closest('form');
            if (form) {
                actualizarPrecioTotal(form);
            }
        });
    });
});
