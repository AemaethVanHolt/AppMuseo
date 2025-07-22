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
    console.log('Actualizando precio total...');
    if (!form) {
        console.error('No se encontró el formulario');
        return;
    }
    
    // Obtener el precio base
    const precioBaseInput = form.querySelector('input[name="precioUnitario"]') || form.querySelector('input[name="precio"]');
    if (!precioBaseInput) {
        console.error('No se encontró el campo de precio base');
        return;
    }
    
    // Obtener la cantidad de entradas
    const cantidadInput = form.querySelector('input[name="cantidad"]');
    let cantidad = 1;
    if (cantidadInput && cantidadInput.value) {
        cantidad = parseInt(cantidadInput.value) || 1;
        if (cantidad < 1) cantidad = 1;
    }
    
    console.log('Cantidad de entradas:', cantidad);
    
    // Obtener el precio base
    let precioBase = 0;
    if (precioBaseInput.dataset.precio) {
        precioBase = parseFloat(precioBaseInput.dataset.precio);
    } else if (precioBaseInput.value) {
        precioBase = parseFloat(precioBaseInput.value);
    }
    
    if (isNaN(precioBase)) precioBase = 0;
    console.log('Precio base:', precioBase);
    
    let totalExtras = 0;
    
    // Sumar los precios de los extras seleccionados
    const checkboxes = form.querySelectorAll('input[type="checkbox"][name="extras"]:checked');
    console.log('Extras seleccionados:', checkboxes.length);
    
    checkboxes.forEach(checkbox => {
        const valorExtra = checkbox.value;
        let precioExtra = 0;
        
        // Obtener el precio del extra del data-precio o del mapa de precios
        if (checkbox.dataset.precio) {
            precioExtra = parseFloat(checkbox.dataset.precio);
            console.log(`Extra ${valorExtra}: ${precioExtra} (de data-precio)`);
        } else if (window.preciosExtras && window.preciosExtras[valorExtra] !== undefined) {
            precioExtra = parseFloat(window.preciosExtras[valorExtra]);
            console.log(`Extra ${valorExtra}: ${precioExtra} (de preciosExtras)`);
        }
        
        totalExtras += precioExtra;
    });
    
    console.log('Total extras por entrada:', totalExtras);
    
    // Calcular el subtotal de los extras
    const subtotalExtras = totalExtras * cantidad;
    
    // Calcular el total (precio base * cantidad + subtotal de extras)
    const subtotalEntradas = precioBase * cantidad;
    const total = subtotalEntradas + subtotalExtras;
    
    console.log('Subtotal entradas:', subtotalEntradas);
    console.log('Subtotal extras:', subtotalExtras);
    console.log('Total:', total);
    
    console.log('Precio base:', precioBase, 'Extras:', totalExtras, 'Total:', total);
    
    // Actualizar el precio mostrado
    const precioElement = form.querySelector('.precio-total, #total');
    if (precioElement) {
        precioElement.textContent = formatearPrecio(total);
        console.log('Precio actualizado en el elemento:', precioElement.textContent);
    } else {
        console.error('No se encontró el elemento para mostrar el precio total');
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
    
    console.log('Precio total guardado:', precioTotalInput.value);
    
    // Actualizar también el importe de transferencia si existe
    const importeTransferencia = document.getElementById('importeTransferencia');
    if (importeTransferencia) {
        importeTransferencia.textContent = formatearPrecio(total);
    }
    
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
        // Verificar si estamos en la página de Taquilla (no mostrar total)
        if (!window.location.pathname.includes('Taquilla/ProcesarCompra')) {
            // No mostrar el total en la página principal de Taquilla
            actualizarPrecioTotal(form);
        } else {
            // Mostrar el total en la página de ProcesarCompra
            const precioBaseElement = form.querySelector('h3 strong');
            if (precioBaseElement) {
                const precioTotalElement = document.createElement('div');
                precioTotalElement.className = 'precio-total-container mt-2';
                precioTotalElement.innerHTML = `<strong>Total: </strong><span class="precio-total">${precioBaseElement.textContent}</span>`;
                precioBaseElement.parentNode.insertBefore(precioTotalElement, precioBaseElement.nextSibling);
                
                // Actualizar el precio total inicial
                actualizarPrecioTotal(form);
            }
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
