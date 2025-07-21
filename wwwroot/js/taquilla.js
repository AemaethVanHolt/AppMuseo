// Manejar cambios en el selector de tipo de descuento
document.addEventListener('DOMContentLoaded', function() {
    const tipoDescuento = document.getElementById('tipoDescuento');
    const precioReducido = document.getElementById('precioReducido');
    const precioReducidoInput = document.getElementById('precioReducidoInput');
    const tipoDescuentoInput = document.getElementById('tipoDescuentoInput');
    const requisitosDescuento = document.getElementById('requisitosDescuento');
    
    if (tipoDescuento) {
        // Actualizar requisitos según el tipo de descuento seleccionado
        function actualizarRequisitos() {
            const valor = tipoDescuento.value;
            let requisitos = '';
            
            switch(valor) {
                case 'estudiante':
                    requisitos = '<li>Carnet de estudiante o matrícula vigente</li>';
                    break;
                case 'investigador':
                    requisitos = '<li>Acreditación de investigador o docente</li>';
                    break;
                case 'discapacidad':
                    requisitos = '<li>Certificado de discapacidad</li><li>Documento de identidad del titular</li>';
                    break;
            }
            
            requisitosDescuento.innerHTML = requisitos;
            
            // Actualizar el valor oculto del formulario
            const precio = tipoDescuento.options[tipoDescuento.selectedIndex].dataset.precio;
            precioReducido.textContent = precio.replace('.', ',');
            precioReducidoInput.value = precio;
            tipoDescuentoInput.value = valor;
        }
        
        tipoDescuento.addEventListener('change', actualizarRequisitos);
        actualizarRequisitos(); // Inicializar al cargar la página
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
