System.register([], function (exports, context) {
    let React, ReactDOM;

    return {
        setters: [],
        execute: function () {
            console.log('React app module executing');

            exports({
                bootstrap: async function () {
                    console.log('React app bootstrapping');
                    try {
                        const reactModule = await System.import('react');
                        const reactDomModule = await System.import('react-dom');

                        React = reactModule.default || reactModule;
                        ReactDOM = reactDomModule.default || reactDomModule;

                        console.log('React app bootstrapped');
                        return Promise.resolve();
                    } catch (err) {
                        console.error('Error bootstrapping React app:', err);
                        return Promise.reject(err);
                    }
                },

                mount: async function () {
                    console.log('React app mounting');
                    try {
                        const App = React.createElement('div', {
                            style: {
                                padding: '20px',
                                backgroundColor: '#f0f0f0',
                                borderRadius: '8px',
                                margin: '10px'
                            }
                        }, [
                            React.createElement('h2', {
                                style: { color: '#2563eb' }
                            }, 'React Component'),
                            React.createElement('p', {}, 'This React component is hosted within your Blazor app using Single-SPA'),
                            React.createElement('button', {
                                style: {
                                    backgroundColor: '#2563eb',
                                    color: 'white',
                                    padding: '8px 16px',
                                    borderRadius: '4px',
                                    border: 'none',
                                    cursor: 'pointer'
                                },
                                onClick: () => alert('Hello from React!')
                            }, 'Click Me')
                        ]);

                        const domElementId = 'single-spa-application:@winzor/react-app';
                        console.log('Looking for DOM element with ID:', domElementId);
                        const domElement = document.getElementById(domElementId);

                        if (!domElement) {
                            console.error('Mount element not found with ID:', domElementId);
                            return Promise.reject(new Error('Mount element not found'));
                        }

                        console.log('Rendering React component to:', domElement);
                        ReactDOM.render(App, domElement);
                        return Promise.resolve();
                    } catch (err) {
                        console.error('Error mounting React app:', err);
                        return Promise.reject(err);
                    }
                },

                unmount: async function () {
                    console.log('React app unmounting');
                    try {
                        const domElement = document.getElementById('single-spa-application:@winzor/react-app');
                        if (domElement) {
                            ReactDOM.unmountComponentAtNode(domElement);
                        }
                        return Promise.resolve();
                    } catch (err) {
                        console.error('Error unmounting React app:', err);
                        return Promise.reject(err);
                    }
                }
            });
        }
    };
});