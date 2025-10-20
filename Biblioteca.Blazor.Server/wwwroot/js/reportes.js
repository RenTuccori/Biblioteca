window.downloadPDF = (reporteDataJson) => {
    const reporteData = JSON.parse(reporteDataJson);
    
    // Crear el contenido HTML del reporte
    const htmlContent = `
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
            <title>Reporte de Préstamos - Biblioteca</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    margin: 20px;
                    color: #333;
                }
                .header {
                    text-align: center;
                    margin-bottom: 30px;
                    border-bottom: 2px solid #007bff;
                    padding-bottom: 20px;
                }
                .header h1 {
                    color: #007bff;
                    margin: 0;
                }
                .info-section {
                    margin-bottom: 30px;
                }
                .info-row {
                    display: flex;
                    justify-content: space-between;
                    margin-bottom: 10px;
                }
                .estadisticas {
                    display: grid;
                    grid-template-columns: repeat(4, 1fr);
                    gap: 20px;
                    margin-bottom: 30px;
                }
                .stat-card {
                    border: 1px solid #ddd;
                    border-radius: 8px;
                    padding: 15px;
                    text-align: center;
                    background-color: #f8f9fa;
                }
                .stat-card h3 {
                    margin: 0 0 10px 0;
                    font-size: 24px;
                    color: #007bff;
                }
                .stat-card p {
                    margin: 0;
                    font-size: 14px;
                    color: #666;
                }
                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 20px;
                }
                th, td {
                    border: 1px solid #ddd;
                    padding: 8px;
                    text-align: left;
                    font-size: 12px;
                }
                th {
                    background-color: #007bff;
                    color: white;
                    font-weight: bold;
                }
                tr:nth-child(even) {
                    background-color: #f2f2f2;
                }
                .badge {
                    padding: 4px 8px;
                    border-radius: 4px;
                    color: white;
                    font-size: 10px;
                    font-weight: bold;
                }
                .badge-success { background-color: #28a745; }
                .badge-danger { background-color: #dc3545; }
                .badge-info { background-color: #17a2b8; }
                .footer {
                    margin-top: 30px;
                    text-align: center;
                    font-size: 12px;
                    color: #666;
                    border-top: 1px solid #ddd;
                    padding-top: 20px;
                }
                @media print {
                    body { margin: 0; }
                    .header { page-break-after: avoid; }
                    table { page-break-inside: avoid; }
                }
            </style>
        </head>
        <body>
            <div class="header">
                <h1>Reporte de Préstamos</h1>
                <h2>Sistema de Biblioteca</h2>
            </div>
            
            <div class="info-section">
                <div class="info-row">
                    <strong>Período:</strong>
                    <span>${reporteData.FechaInicio} - ${reporteData.FechaFin}</span>
                </div>
                <div class="info-row">
                    <strong>Fecha de generación:</strong>
                    <span>${reporteData.FechaGeneracion}</span>
                </div>
            </div>

            <div class="estadisticas">
                <div class="stat-card">
                    <h3>${reporteData.Estadisticas.TotalPrestamos || 0}</h3>
                    <p>Total Préstamos</p>
                </div>
                <div class="stat-card">
                    <h3>${reporteData.Estadisticas.Devueltos || 0}</h3>
                    <p>Devueltos</p>
                </div>
                <div class="stat-card">
                    <h3>${reporteData.Estadisticas.Activos || 0}</h3>
                    <p>Activos</p>
                </div>
                <div class="stat-card">
                    <h3>${reporteData.Estadisticas.Vencidos || 0}</h3>
                    <p>Vencidos</p>
                </div>
            </div>

            <h3>Detalle de Préstamos</h3>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Libro</th>
                        <th>Autor</th>
                        <th>Socio</th>
                        <th>Fecha Préstamo</th>
                        <th>Fecha Devolución Prevista</th>
                        <th>Fecha Devolución Real</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody>
                    ${reporteData.Prestamos.map(prestamo => `
                        <tr>
                            <td>${prestamo.PrestamoId}</td>
                            <td>${prestamo.LibroTitulo}</td>
                            <td>${prestamo.AutorNombre}</td>
                            <td>${prestamo.SocioNombre}</td>
                            <td>${prestamo.FechaPrestamo}</td>
                            <td>${prestamo.FechaDevolucionPrevista}</td>
                            <td>${prestamo.FechaDevolucionReal}</td>
                            <td>
                                <span class="badge ${prestamo.Estado === 'Devuelto' ? 'badge-success' : 
                                                    prestamo.Estado === 'Vencido' ? 'badge-danger' : 
                                                    'badge-info'}">
                                    ${prestamo.Estado}
                                </span>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>

            <div class="footer">
                <p>Reporte generado por el Sistema de Biblioteca</p>
                <p>Este documento contiene información confidencial del sistema.</p>
            </div>
        </body>
        </html>
    `;

    // Crear una ventana nueva para el contenido
    const printWindow = window.open('', '_blank');
    printWindow.document.open();
    printWindow.document.write(htmlContent);
    printWindow.document.close();
    
    // Esperar un poco para que se cargue el contenido y luego imprimir
    setTimeout(() => {
        printWindow.print();
        
        // Opcional: cerrar la ventana después de imprimir
        printWindow.addEventListener('afterprint', () => {
            printWindow.close();
        });
    }, 500);
};