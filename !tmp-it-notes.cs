# Init VUE app

const app = Vue.createApp({
	data() {
		return {
			product: 'Socks'
		}
	}
})

<div id="app">{{ product }}</div>

const mountedApp = app.mount('#app');

mountedApp.product = 'Shoes'; // console change value