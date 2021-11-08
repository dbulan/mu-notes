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


# ---------- AlpineJS
https://maxsite.org/page/alpine-js
Habr: https://habr.com/ru/post/504650/ part-1
Habr: https://habr.com/ru/post/505954/ part-2


Рекомендуется: установите Alpine IntelliSense для VsCode, чтобы он давал вам соответствующие автозаполнения


Когда лучше избегать Alpine:
	Вы НЕ должны обращаться к библиотекам, таким как alpine, когда ваш сайт:
		- Много манипуляций с данными
		- Требуются огромные проверки.
		- Приходится выполнять многочисленные вызовы API для получения и обработки данных.

Когда использовать Alpine:
	Вам следует обращаться к таким библиотекам, как alpine, только если ваш сайт:
		- Придется проделать базовые манипуляции с DOM.
		- При некоторых условиях вам необходимо добавлять классы при взаимодействии с пользователем.
		- Прослушайте некоторые события и измените пользовательский интерфейс.


// The important thing is that Alpine.js can be loaded at the end of the BODY, which gives an increase in page speed. 
// If we have to load jQuery (and its analogs) into the HEAD section (since its functions can be arbitrarily found in the body of the page), then with Alpine.js we don't think about it at all.

<script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v2.x.x/dist/alpine.min.js"></script>

// toggle class name
<div x-data="{t: false}">
	<div x-on:click="t = !t" x-bind:class="{ 't-red': t }">Toggle class click</div>
</div>
//
<div x-data="{t: false}">
	<div @click="t = !t" :class="{ 't-red': t }">Toogle class click</div>
</div>

# Dropdown (also for modals)

<div x-data="{ open: false }" class="pos-relative b-inline">
    <button @click="open = true" class="button button1">Open</button>
    
    <div x-show="open" @click.away="open = false" class="animation-fade pad20 bordered pos-absolute w200px z-index1 bg-white" x-cloak>
    	Content
    </div>
</div>

<style>
	[x-cloak] {display: none;} // prevent element show while page loads
</style>

# Tabs

<div x-data="{ tab: 'foo' }">
    <button :class="{ 'bg-blue': tab === 'foo' }" @click="tab = 'foo'">Foo</button>
    <button :class="{ 'bg-blue': tab === 'bar' }" @click="tab = 'bar'">Bar</button>
 
	<div class="pad20 bordered">
    	<div x-show="tab === 'foo'" class="animation-fade">Вкладка Foo</div>
    	<div x-show="tab === 'bar'" class="animation-fade" x-cloak>Вкладка Bar</div>
    </div>
</div>

# Input char counter

<div x-data="{
	content: '',
	limit: 180,
	remaining() {
		return this.limit - this.content.length
	}
}">
	<textarea x-model="content"></textarea>
	<p>You have <span x-html="remaining()"></span> character left!</p>
</div>

# TODO

<div
  x-data="{ todos: [{id: 1, title: 'купить хлеб', completed: false}, {id: 2, title: 'продать айфон', completed: false}, {id: 3, title: 'закончить этот курс', completed: false}, {id: 4, title: 'перестать быть банальным', completed: false}] }"
>
  <h1>Планы на сегодня:</h1>
  <ul>
    <template x-for="todo in todos" :key="todo.id">
      <li x-text="todo.title"></li>
    </template>
  </ul>
</div>

# X-INIT

// X-init — директива, получающая блок кода, который вы хотите выполнить во время инициации компонента. 
// Похожа на использование created во Vue.js. Основное применение это вычисление начальных значений в x-data.

<div
  x-data="{ posts: [] }"
  x-init="
    fetch('https://jsonplaceholder.typicode.com/albums/1/photos')
      .then((response) => {
        return response.json(); // преобразования в json-формат
      })
      .then((data) => {
        posts = data	// присваиваем нашему объекту
      });
    "
>
  <template x-for="post in posts" :key="post.id">
    <div class="post">
      <h1 x-text="post.title"></h1>
      <img :src="post.thumbnailUrl" :alt="post.title" />
    </div>
  </template>
  <hr/>
</div>

// X-REF и $REFS

Эта директива предоставляет доступ к нужному элементу в любой части страницы, в том числе и вне компонента. 
Все ссылки становятся доступными через глобальное поле $refs.

<div x-data="{ }">
  <input type="text" x-ref="myInput"/>
  <button x-on:click="alert($refs.myInput.value)">Alert</button>
<hr />
</div>

// Текст введенный в input с атрибутом x-ref=«myInput» будет доступен в $refs.myInput.value