<script setup>
import {reactive} from 'vue';

const state = reactive({msg: ''})

const loginJson = () => fetch('https://aspnetcorespacookiesxsrf.company.local/login',{
  method: 'POST',
  headers: {
    'Content-Type' : 'application/json',
  },
  credentials: "include",
  body: JSON.stringify( value, {
    Username: 'foo',
    Password: 'pw',
  })
})

const secret = () => fetch('https://api.company.local/protected', {
 credentials: "include",
})

.then(x => x.text())
.then(t=> state.msg = t)

const get = () => fetch('https://api.company.local/protected', {
 credentials: "include",
})

.then(x => x.text())
.then(t=> state.msg = t)
</script>

<template>
  <h1>App</h1>
  <div>
    {{state.msg}}
  </div>
  <main>
    <TheWelcome />
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
