$(document).ready(function (){
    console.log('=== MAIN.JS DEBUG ===');
    console.log('main.js yükləndi');
    console.log('Bootstrap:', typeof bootstrap !== 'undefined' ? 'YÜKLƏNİB ✓' : 'YÜKLƏNMƏYİB ✗');
    console.log('jQuery:', typeof $ !== 'undefined' ? 'YÜKLƏNİB ✓' : 'YÜKLƏNMƏYİB ✗');
    console.log('bookModalIcon sayı:', $('.bookModalIcon').length);
    
    // Bütün bookModalIcon elementlərini göstər
    $('.bookModalIcon').each(function(index) {
        console.log('bookModalIcon #' + index + ':', $(this).attr('href'));
    });
    
    // Event delegation istifadə edirik - document-ə bağlayırıq
    $(document).on('click', '.bookModalIcon', function (e){
        e.preventDefault();
        console.log('=== MODAL AÇILIR ===');
        console.log('bookModalIcon click olundu!');
        
        //url /books/bookmodal/id
        let url = $(this).attr('href');
        console.log('URL:', url);
        
        fetch(url)
            .then(response => {
                console.log('Response status:', response.status);
                if (!response.ok) {
                    throw new Error('HTTP status ' + response.status);
                }
                return response.text();
            })
            .then(data => {
                console.log('Data yükləndi:', data.length, 'characters');
                $("#quickModal .modal-dialog").html(data);

                // Bootstrap yüklənib yoxsa yoxla
                if (typeof bootstrap === 'undefined') {
                    console.error('❌ Bootstrap JS yüklənməyib!');
                    alert('Modal açıla bilmədi. Bootstrap yüklənməyib!');
                    return;
                }

                // Bootstrap 5 modal - vanilla JS ilə
                let modalElement = document.getElementById('quickModal');
                console.log('Modal element:', modalElement ? 'TAPILDI ✓' : 'TAPILMADI ✗');
                
                if (!modalElement) {
                    console.error('❌ Modal element tapılmadı!');
                    alert('Modal element (#quickModal) tapılmadı!');
                    return;
                }
                
                let modal = new bootstrap.Modal(modalElement);
                console.log('✓ Modal obyekti yaradıldı');
                
                modal.show();
                console.log('✓ modal.show() çağırıldı');

                // Bir az gözlə ki modal və HTML tam render olsun
                setTimeout(function() {
                    console.log('Slider yaradılır...');
                    
                    // Əgər slider elementləri varsa
                    if ($(".product-details-slider").length > 0) {
                        let firsSlider = {
                             "slidesToShow": 1,
                            "arrows": false,
                            "fade": true,
                            "draggable": false,
                            "swipe": false,
                            "asNavFor": ".product-slider-nav"
                        };
                        let secondSlider = {
                            "infinite": true,
                            "autoplay": true,
                            "autoplaySpeed": 8000,
                            "slidesToShow": 4,
                            "arrows": true,
                            "prevArrow": {"buttonClass": "slick-prev", "iconClass": "fa fa-chevron-left"},
                            "nextArrow": {"buttonClass": "slick-next", "iconClass": "fa fa-chevron-right"},
                            "asNavFor": ".product-details-slider",
                            "focusOnSelect": true
                        };

                        // Yeni slider yarat
                        $(".product-details-slider").slick(firsSlider);
                        $(".product-slider-nav").slick(secondSlider);
                        console.log('✓ Slider yaradıldı');
                    } else {
                        console.log('⚠ Slider elementləri tapılmadı');
                    }
                }, 300);
            })
            .catch(error => {
                console.error('❌ Modal yüklənmə xətası:', error);
                alert('Modal yüklənmə xətası: ' + error.message);
            });
    });
    
    console.log('=== MAIN.JS HAZIR ===');
});
