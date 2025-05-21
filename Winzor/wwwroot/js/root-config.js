(function () {
    console.log("Root config loading");

    if (typeof singleSpa === 'undefined') {
        console.error('Single-SPA not found in global scope as "singleSpa"');

        if (typeof window.SingleSpa !== 'undefined') {
            console.log('Found as window.SingleSpa instead');
            window.singleSpa = window.SingleSpa;
        } else {
            console.error('Single-SPA not available at all');
            return;
        }
    } else {
        console.log('singleSpa found in global scope:', singleSpa);
    }

    singleSpa.registerApplication(
        '@winzor/react-app',
        () => System.import('@winzor/react-app'),
        location => location.pathname.startsWith('/react')
    );

    singleSpa.start();
    console.log('Single-SPA started');
})();